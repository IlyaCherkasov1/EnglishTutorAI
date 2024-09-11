// eslint-disable-next-line @typescript-eslint/no-explicit-any
export const getRoute = (path: string, ...parameters: any[]): string => {
    return parameters?.length ? parameters.reduce((p, parameter) => p.replace(/:[^/]*/, parameter), path) : path;
};