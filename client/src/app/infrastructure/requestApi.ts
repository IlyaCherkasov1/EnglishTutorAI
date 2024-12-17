import {EmptyObject} from "react-hook-form";
import {clearAccessToken, getAccessToken} from "@/app/infrastructure/services/auth/accessTokenService.ts";
import {isAccessTokenExpired, renewAccessTokenHandler} from "@/app/infrastructure/services/auth/identityService.ts";
import {routes} from "@/app/components/layout/routes/routeLink.ts";
import {notifications} from "@/app/components/toast/toast.tsx";
import {contentTypes} from "@/app/infrastructure/constants/contentTypes.ts";
import {headerNames} from "@/app/infrastructure/constants/headerNames.ts";
import {responseHandlingStatuses} from "@/app/infrastructure/constants/responseHandlingStatuses.ts";

export type HttpRequestMethod = "GET" | "POST" | "PUT" | "DELETE" | "PATCH";

export interface RequestOptions<T> {
    url: string;
    body?: T;
    enableCache?: boolean;
    isAnonymous?: boolean;
}

export const apiRootUrl = import.meta.env.VITE_APP_API_URL;

const getContentTypeHeader = <T>(options?: RequestOptions<T>): { "Content-Type": string } | EmptyObject =>
    options === null || options === undefined
        ? { "Content-Type": contentTypes.plainText }
        : options.body instanceof FormData
            ? {}
            : {
                "Content-Type": typeof options.body === "object" ? contentTypes.json : contentTypes.plainText,
            };

const performRequest = async <TRequest, TResult>(
    method: HttpRequestMethod,
    options: RequestOptions<TRequest>
): Promise<TResult> => {
    const authorizationHeader = options.isAnonymous
        ? {}
        : await getAuthorizationHeader(options);

    const response = await fetch(`${apiRootUrl}/${options.url}`, {
        method,
        body: getBody(options),
        credentials: "include",
        headers: {
            Accept: contentTypes.json,
            ...authorizationHeader,
            ...getContentTypeHeader(options),
        },
        cache: options.enableCache ? "force-cache" : "no-cache",
    });

    if (response.ok) {
        return await handleSucceededResponse<TResult>(response)
    } else {
        return await handleFailedResponse(response);
    }
};

const getBody = <T>(options: RequestOptions<T>) => {
    if (!options || !options.body) {
        return undefined;
    }

    return JSON.stringify(options.body);
}

const handleSucceededResponse = async <TResult>(response: Response): Promise<TResult> => {
    const responseText = await response.text();
    return responseText ? JSON.parse(responseText) : ({} as TResult);
};

const handleFailedResponse = async <TResult>(response: Response): Promise<TResult> => {
    const handleHeaderStatus = await handleHeaders(response);

    if (handleHeaderStatus === responseHandlingStatuses.unhandled) {
        notifications.defaultError(response.headers.get(headerNames.exceptionTraceId));
    }

    if (handleHeaderStatus === responseHandlingStatuses.unauthorized) {
        clearAccessToken();
        return {} as TResult;
    }

    throw new Error(
        `Request was failed with status: ${Object.entries(responseHandlingStatuses)
            .map(([key, value]) => (value === handleHeaderStatus ? key : null))
            .filter((key) => key)
            .join(", ")}.`
    );
};

const handleHeaders = async (response: Response): Promise<number> => {
    let handleHeaderStatus = responseHandlingStatuses.unhandled;

    switch (response.status) {
        case 401:
            handleHeaderStatus = await handleUnauthorized(response);
            break;
        case 404:
            handleHeaderStatus = responseHandlingStatuses.notFound;
            break;
    }

    handleRedirect(handleHeaderStatus);

    return handleHeaderStatus;
};

const handleUnauthorized = async (response: Response): Promise<number> =>
    isInvalidTokenResponse(response) ? await renewAccessTokenHandler() : responseHandlingStatuses.unauthorized;

const isInvalidTokenResponse = (response: Response): boolean => {
    const authHeader = response.headers.get(headerNames.authenticate);
    return !!(authHeader && authHeader.includes("invalid_token"));
};

const getAuthorizationHeader = <T>(options: RequestOptions<T>): Promise<{
    Authorization: string
} | EmptyObject | null> =>
    options.isAnonymous ? Promise.resolve(null) : getAccessTokenAuthorizationHeader();

const getAccessTokenAuthorizationHeader = async (): Promise<{ Authorization: string } | EmptyObject> => {
    let token = getAccessToken();

    if (token && isAccessTokenExpired(token)) {
        const refreshTokenResponseStatus = await renewAccessTokenHandler();

        handleRedirect(refreshTokenResponseStatus);
        token = getAccessToken();
    }

    return token ? { Authorization: `Bearer ${token}` } : {};
};

const handleRedirect = (status: number) => {
    let redirectToPage: string | undefined;

    switch (status) {
        case responseHandlingStatuses.unauthorized:
        case responseHandlingStatuses.unauthenticated:
            redirectToPage = routes.login;
            break;
        case responseHandlingStatuses.notFound:
            redirectToPage = routes.translates;
            break;
    }

    if (redirectToPage) {
        window.location.href = redirectToPage;
    }
};

export const httpGet = async <TResult>(options: RequestOptions<void>): Promise<TResult> =>
    performRequest("GET", options);

export const httpPost = async <TRequest, TResult = void | string>(options: RequestOptions<TRequest>): Promise<TResult> =>
    performRequest<TRequest, TResult>("POST", options);

export const httpPut = async <TRequest, TResult = void>(options: RequestOptions<TRequest>): Promise<TResult> =>
    performRequest<TRequest, TResult>("PUT", options);

export const httpDelete = async <TResult = void>(options: RequestOptions<void>): Promise<TResult> =>
    performRequest("DELETE", options);

export const httpPatch = async <TRequest, TResult = void>(options: RequestOptions<TRequest>): Promise<TResult> =>
    performRequest<TRequest, TResult>("PATCH", options);