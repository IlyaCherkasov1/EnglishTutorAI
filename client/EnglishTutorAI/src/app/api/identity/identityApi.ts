import {UserRegisterRequest} from "../../dataModels/identity/userRegisterRequest.ts";
import {httpPost} from "../../infrastructure/requestApi.ts";
import {LoginRequest} from "../../dataModels/identity/loginRequest.ts";
import {LoginResponse} from "../../dataModels/identity/loginResponse.ts";
import {Result} from "../../dataModels/result.ts";

const identityResource = "identity";

export const registerUser = async (request: UserRegisterRequest): Promise<Result> => {
    return httpPost({
        url: `${identityResource}/register`,
        body: request,
    });
}

export const loginUser = async (request: LoginRequest): Promise<Result<LoginResponse>> => {
    return httpPost({
        url: `${identityResource}/login`,
        body: request,
    });
}