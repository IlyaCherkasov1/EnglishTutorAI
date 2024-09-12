import {IdentityUserResponse} from "../../dataModels/user/identityUserResponse.ts";
import {httpGet} from "../../infrastructure/requestApi.ts";

const userResource = "identity";

export const getUser = async (): Promise<IdentityUserResponse> => {
    return httpGet({
        url: `${userResource}/get-user`
    })
}