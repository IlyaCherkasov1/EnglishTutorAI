import {clearAccessToken, setAccessToken} from "./accessTokenService.ts";
import {jwtDecode} from "jwt-decode";
import {responseHandlingStatuses} from "../../requestApi.ts";
import {renewAccessToken} from "../../../api/identity/identityApi.ts";

export const applyNewIdentity = async (accessToken: string) => {
    setAccessToken(accessToken);
}

export const isAccessTokenExpired = (accessToken: string) => {
    const decodedToken: { exp: number } = jwtDecode(accessToken);

    return Date.now() > decodedToken.exp * 1000;
};

export const refreshToken = async (): Promise<number> => {
    const response = await renewAccessToken();

    if (!response.isSucceeded) {
        return responseHandlingStatuses.unauthenticated;
    }

    setAccessToken(response.data)

    return responseHandlingStatuses.refreshTokenWasCompleted;
}

export const removeIdentityData = () => {
    clearAccessToken();
};

export function performAfterLogOutActions() {
    removeIdentityData();
}