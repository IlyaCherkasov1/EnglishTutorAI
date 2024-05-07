export type HttpRequestMethod = "GET" | "POST" | "PUT" | "DELETE" | "PATCH";

export interface RequestOptions<T> {
    url: string;
    body?: T;
}

export const apiRootUrl = "https://localhost:7008/api";

const contentTypes = {
    plainText: "text/plain",
    json: "application/json",
};

function getBody<T>(options: RequestOptions<T>) {
    if (!options || !options.body) {
        return undefined;
    }

    if (options.body instanceof FormData) {
        return options.body;
    }

    return JSON.stringify(options.body);
}

function getContentTypeHeader<T>(options?: RequestOptions<T>): { "Content-Type": string } | {} {
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

async function handleResponse(response: Response): Promise<any> {
    if (!response.ok) {
        throw new Error("error on performing API request")
    }

    const text = await response.text();
    return text ? JSON.parse(text) : {};
}

async function performRequest<TRequest, TResult>(
    method: HttpRequestMethod,
    options: RequestOptions<TRequest>
): Promise<TResult> {
        const response = await fetch(`${apiRootUrl}/${options.url}`, {
            method,
            body: getBody(options),
            credentials: "same-origin",
            headers: {
                Accept: contentTypes.json,
                ...getContentTypeHeader(options),
            },
        });

    return await handleResponse(response);
}


export async function httpGet<TResult>(options: RequestOptions<void>): Promise<TResult> {
    return performRequest("GET", options);
}

export async function httpPost<TRequest, TResult = void>(options: RequestOptions<TRequest>): Promise<TResult> {
    return performRequest<TRequest, TResult>("POST", options);
}

export async function httpPut<TRequest, TResult = void>(options: RequestOptions<TRequest>): Promise<TResult> {
    return performRequest<TRequest, TResult>("PUT", options);
}

export async function httpDelete<TResult = void>(options: RequestOptions<void>): Promise<TResult> {
    return performRequest("DELETE", options);
}

export async function httpPatch<TRequest, TResult = void>(options: RequestOptions<TRequest>): Promise<TResult> {
    return performRequest<TRequest, TResult>("PATCH", options);
}