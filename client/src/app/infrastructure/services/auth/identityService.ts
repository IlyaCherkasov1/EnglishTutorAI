import {clearAccessToken, setAccessToken} from "./accessTokenService.ts";
import {LoginResponse} from "../../../dataModels/identity/loginResponse.ts";
import {jwtDecode} from "jwt-decode";
import {responseHandlingStatuses} from "../../requestApi.ts";

export const applyNewIdentity = async (data: LoginResponse) => {
    setAccessToken(data.accessToken);
}

export const isAccessTokenExpired = (accessToken: string) => {
    const decodedToken: { exp: number } = jwtDecode(accessToken);

    return Date.now() > decodedToken.exp * 1000;
};

export async function refreshToken(): Promise<number> {
    // TODO: Implement refresh token functionality
    return responseHandlingStatuses.unauthenticated;
}

export const removeIdentityData = () => {
    clearAccessToken();
};

export function performAfterLogOutActions() {
    removeIdentityData();
}