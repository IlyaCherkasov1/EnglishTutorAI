import {httpGet} from "@/app/infrastructure/requestApi";
import {IdentityUserResponse} from "@/app/dataModels/user/identityUserResponse";

const userResource = "identity";

export const getUser = async (): Promise<IdentityUserResponse> => {
    return httpGet({
        url: `${userResource}/get-user`
    })
}