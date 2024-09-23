import {clearAccessToken, setAccessToken} from "./accessTokenService.ts";
import {jwtDecode} from "jwt-decode";
import {responseHandlingStatuses} from "../../requestApi.ts";
import {logout, renewAccessToken} from "../../../api/identity/identityApi.ts";

export const applyNewIdentity = async (accessToken: string) => {
    setAccessToken(accessToken);
}

export const isAccessTokenExpired = (accessToken: string) => {
    const decodedToken: { exp: number } = jwtDecode(accessToken);

    return Date.now() > decodedToken.exp * 1000;
};

export const renewAccessTokenHandler = async (): Promise<number> => {
    const response = await renewAccessToken();

    if (!response.isSucceeded) {
        return responseHandlingStatuses.unauthenticated;
    }

    setAccessToken(response.data)

    return responseHandlingStatuses.refreshTokenWasCompleted;
}

export const performLogOut = async (): Promise<void> => {
    await logout();
    performAfterLogOutActions();
}

export const performAfterLogOutActions = () => {
    clearAccessToken();
    location.reload();
}