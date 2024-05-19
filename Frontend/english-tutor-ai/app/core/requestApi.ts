export type HttpRequestMethod = "GET" | "POST" | "PUT" | "DELETE" | "PATCH";

export interface RequestOptions<T> {
    url: string;
    body?: T;
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

const handleResponse = async (response: Response): Promise<any> => {
    if (!response.ok) {
        throw new Error("error on performing API request")
    }

    const text = await response.text();
    return text ? JSON.parse(text) : {};
}

const performRequest = async <TRequest, TResult>(
    method: HttpRequestMethod,
    options: RequestOptions<TRequest>
): Promise<TResult> => {
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


export const httpGet = async <TResult>(options: RequestOptions<void>): Promise<TResult> => {
    return performRequest("GET", options);
}

export const httpPost = async <TRequest, TResult = void | string>(options: RequestOptions<TRequest>): Promise<TResult> => {
    return performRequest<TRequest, TResult>("POST", options);
}

export const httpPut = async <TRequest, TResult = void>(options: RequestOptions<TRequest>): Promise<TResult> => {
    return performRequest<TRequest, TResult>("PUT", options);
}

export const httpDelete  = async <TResult = void>(options: RequestOptions<void>): Promise<TResult> => {
    return performRequest("DELETE", options);
}

export const httpPatch = async <TRequest, TResult = void>(options: RequestOptions<TRequest>): Promise<TResult> => {
    return performRequest<TRequest, TResult>("PATCH", options);
}