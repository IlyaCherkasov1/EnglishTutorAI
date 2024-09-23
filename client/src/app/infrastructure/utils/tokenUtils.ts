import {isAccessTokenExpired} from "@/app/infrastructure/services/auth/identityService.ts";
import {getAccessToken} from "@/app/infrastructure/services/auth/accessTokenService.ts";

export const isAccessTokenValid = () => {
    const accessToken = getAccessToken();
    return accessToken && !isAccessTokenExpired(accessToken);
};