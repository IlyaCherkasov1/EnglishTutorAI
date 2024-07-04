import { httpGet, httpPost } from "@/app/infrastructure/requestApi";
import {UserRegisterRequest} from "@/app/dataModels/identity/userRegisterRequest";
import {LoginRequest} from "@/app/dataModels/identity/loginRequest";
import {RefreshTokenRequest} from "@/app/dataModels/identity/refreshTokenRequest";
import {LoginResponse} from "@/app/dataModels/identity/loginResponse";

const identityResource = "identity";

export const register = async (request: UserRegisterRequest): Promise<any> => {
    return httpPost({
        url: `${identityResource}/register`,
        body: request,
        skipHandleResponse: true,
    });
}

export const login = async (request: LoginRequest): Promise<LoginResponse> => {
    return httpPost({
        url: `${identityResource}/login`,
        body: request,
    });
}

export const refreshToken = async (request: RefreshTokenRequest) => {
    return httpPost({
        url: `${identityResource}/refresh`,
        body: request,
    });
}

// export const confirmEmail = async (userId, code) => {
//     return httpGet({
//         url: `${identityResource}/confirmEmail`,
//         params: { userId, code },
//     });
// }

export const resendConfirmationEmail = async (request) => {
    return httpPost({
        url: `${identityResource}/resendConfirmationEmail`,
        body: request,
    });
}

export const forgotPassword = async (request) => {
    return httpPost({
        url: `${identityResource}/forgotPassword`,
        body: request,
    });
}

export const resetPassword = async (request) => {
    return httpPost({
        url: `${identityResource}/resetPassword`,
        body: request,
    });
}

export const enable2FA = async (request) => {
    return httpPost({
        url: `${identityResource}/manage/2fa`,
        body: request,
    });
}

export const getUserInfo = async () => {
    return httpGet({
        url: `${identityResource}/manage/info`,
    });
}

export const updateUserInfo = async (request) => {
    return httpPost({
        url: `${identityResource}/manage/info`,
        body: request,
    });
}