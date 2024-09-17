import {UserRegisterRequest} from "../../dataModels/identity/userRegisterRequest.ts";
import {httpGet, httpPost} from "../../infrastructure/requestApi.ts";
import {LoginRequest} from "../../dataModels/identity/loginRequest.ts";
import {Result} from "../../dataModels/result.ts";

const identityResource = "identity";

export const registerUser = async (request: UserRegisterRequest): Promise<Result> => {
    return httpPost({
        url: `${identityResource}/register`,
        body: request,
    });
}

export const loginUser = async (request: LoginRequest): Promise<Result<string>> => {
    return httpPost({
        url: `${identityResource}/login`,
        body: request,
    });
}

export const renewAccessToken = async (): Promise<Result<string>> => {
    return httpGet({
        url: `${identityResource}/renewAccessToken`,
        isRefreshTokenRequest: true,
    })
}

export const logout = async (): Promise<Result<string>> => {
    return httpGet({
        url: `${identityResource}/logout`,
    })
}