import {EmptyObject} from "react-hook-form";
import {getAccessToken} from "./services/auth/accessTokenService.ts";
import {isAccessTokenExpired, performAfterLogOutActions, refreshToken} from "./services/auth/identityService.ts";
import {routeLinks} from "../components/layout/routes/routeLink.ts";

export type HttpRequestMethod = "GET" | "POST" | "PUT" | "DELETE" | "PATCH";

export interface RequestOptions<T> {
    url: string;
    body?: T;
    enableCache?: boolean;
    isAnonymous?: boolean;
}

export const responseHandlingStatuses = {
    unhandled: 0,
    unauthenticated: 1,
    refreshTokenWasFailed: 2,
    unauthorized: 3,
};

export const apiRootUrl = import.meta.env.VITE_APP_API_URL;

const headerNames = {
    authenticate: "www-authenticate",
};

const contentTypes = {
    plainText: "text/plain",
    json: "application/json",
};

const getBody = <T>(options: RequestOptions<T>) =>
    !options || !options.body
        ? undefined
        : options.body instanceof FormData
            ? options.body
            : JSON.stringify(options.body);

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
    const authorizationHeader = await getAuthorizationHeader(options);

    const response = await fetch(`${apiRootUrl}/${options.url}`, {
        method,
        body: getBody(options),
        credentials: "same-origin",
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

const handleSucceededResponse = async <TResult>(response: Response): Promise<TResult> => {
    const responseText = await response.text();
    return responseText ? JSON.parse(responseText) : ({} as TResult);
};

const handleFailedResponse = async <TResult>(response: Response): Promise<TResult> => {
    const handleHeaderStatus = await handleHeaders(response);

    if (handleHeaderStatus === responseHandlingStatuses.unauthorized) {
        performAfterLogOutActions();
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
    }

    handleRedirect(handleHeaderStatus);

    return handleHeaderStatus;
};

const handleUnauthorized = async (response: Response): Promise<number> =>
    isInvalidTokenResponse(response) ? await refreshToken() : responseHandlingStatuses.unauthorized;

const isInvalidTokenResponse = (response: Response): boolean => {
    const authHeader = response.headers.get(headerNames.authenticate);
    return !!(authHeader && authHeader.includes("invalid_token"));
};

const getAuthorizationHeader = <T>(options: RequestOptions<T>): Promise<{
    Authorization: string
} | EmptyObject | null> =>
    options.isAnonymous ? Promise.resolve(null) : getAccessTokenAuthorizationHeader();

const getAccessTokenAuthorizationHeader = async (): Promise<{ Authorization: string } | EmptyObject> => {
    const token = getAccessToken();
    if (token && isAccessTokenExpired(token)) {
        // TODO: fetch refresh token
    }
    return token ? { Authorization: `Bearer ${token}` } : {};
};

const handleRedirect = (status: number) => {
    let redirectToPage: string | undefined;

    switch (status) {
        case responseHandlingStatuses.unauthorized:
        case responseHandlingStatuses.unauthenticated:
            redirectToPage = routeLinks.login;
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