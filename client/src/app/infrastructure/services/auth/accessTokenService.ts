let accessToken: string | null;

export const getAccessToken = (): string | null => {
    return accessToken;
}

export const setAccessToken = (token: string): void => {
    accessToken = token;
}

export const clearAccessToken = (): void => {
    accessToken = null;
}