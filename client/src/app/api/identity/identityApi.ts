import {UserRegisterRequest} from "@/app/dataModels/identity/userRegisterRequest.ts";
import {httpGet, httpPost} from "@/app/infrastructure/requestApi.ts";
import {LoginRequest} from "@/app/dataModels/identity/loginRequest.ts";
import {Result} from "@/app/dataModels/result.ts";

const identityResource = "identity";

export const registerUser = async (request: UserRegisterRequest): Promise<Result> => {
    return httpPost({
        url: `${identityResource}/register`,
        body: request,
        isAnonymous: true,
    });
}

export const loginUser = async (request: LoginRequest): Promise<Result<string>> => {
    return httpPost({
        url: `${identityResource}/login`,
        body: request,
        isAnonymous: true,
    });
}

export const renewAccessToken = async (): Promise<Result<string>> => {
    return httpGet({
        url: `${identityResource}/renewAccessToken`,
        isAnonymous: true,
    })
}

export const logout = async (): Promise<Result<string>> => {
    return httpGet({
        url: `${identityResource}/logout`,
    })
}