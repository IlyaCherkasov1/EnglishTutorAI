import {getAccessToken} from "@/app/infrastructure/utils/sessionUtils";
import {opt} from "ts-interface-checker";

export type HttpRequestMethod = "GET" | "POST" | "PUT" | "DELETE" | "PATCH";

export interface RequestOptions<T> {
    url: string;
    body?: T;
    enableCache?: boolean;
    isAnonymous?: boolean;
    skipHandleResponse?: boolean;
}

export const apiRootUrl = process.env.NEXT_PUBLIC_LOCAL_API_URL;

const contentTypes = {
    plainText: "text/plain",
    json: "application/json",
};

const getBody = <T>(options: RequestOptions<T>) => {
    if (!options || !options.body) {
        return undefined;
    }

    if (options.body instanceof FormData) {
        return options.body;
    }

    return JSON.stringify(options.body);
}

const getContentTypeHeader = <T>(options?: RequestOptions<T>): { "Content-Type": string } | {} => {
    if (options === null || options === undefined) {
        return { "Content-Type": contentTypes.plainText };
    }
    if (options.body instanceof FormData) {
        return {};
    }
    return {
        "Content-Type": typeof options.body === "object" ? contentTypes.json : contentTypes.plainText,
    };
}

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

    if (options.skipHandleResponse) {
        return response as TResult;
    } else {
        return await handleResponse(response);
    }
}

const handleResponse = async (response: Response): Promise<any> => {
    if (!response.ok) {
        throw new Error("error on performing API request")
    }

    const text = await response.text();
    return text ? JSON.parse(text) : {};
}

async function getAuthorizationHeader<T>(options: RequestOptions<T>): Promise<{ Authorization: string } | {} | null> {
    if (options.isAnonymous) {
        return null;
    }

    return getAccessTokenAuthorizationHeader();
}

async function getAccessTokenAuthorizationHeader(): Promise<{ Authorization: string } | {}> {
    let token = await getAccessToken();

    return token ? { Authorization: `Bearer ${token}` } : {};
}


export const httpGet = async <TResult>(options: RequestOptions<void>): Promise<TResult> => {
    return performRequest("GET", options);
}

export const httpPost = async <TRequest, TResult = void | string>(options: RequestOptions<TRequest>): Promise<TResult> => {
    return performRequest<TRequest, TResult>("POST", options);
}

export const httpPut = async <TRequest, TResult = void>(options: RequestOptions<TRequest>): Promise<TResult> => {
    return performRequest<TRequest, TResult>("PUT", options);
}

export const httpDelete = async <TResult = void>(options: RequestOptions<void>): Promise<TResult> => {
    return performRequest("DELETE", options);
}

export const httpPatch = async <TRequest, TResult = void>(options: RequestOptions<TRequest>): Promise<TResult> => {
    return performRequest<TRequest, TResult>("PATCH", options);
}