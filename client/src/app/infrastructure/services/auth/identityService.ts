import {clearAccessToken, setAccessToken} from "@/app/infrastructure/services/auth/accessTokenService.ts";
import {jwtDecode} from "jwt-decode";
import {responseHandlingStatuses} from "@/app/infrastructure/requestApi.ts";
import {logout, renewAccessToken} from "@/app/api/identity/identityApi.ts";
import {routeLinks} from "@/app/components/layout/routes/routeLink.ts";

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
    window.location.pathname = routeLinks.login;
}