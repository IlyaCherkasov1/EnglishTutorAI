import qs from "qs";

export const objectToQueryString = (obj: any): string => {
    return qs.stringify(obj, { addQueryPrefix: true });
}

export const queryStringToObject = (queryString: string): any => {
    return qs.parse(queryString, { ignoreQueryPrefix: true });
}