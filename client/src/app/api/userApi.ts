import {httpPost} from "@/app/infrastructure/requestApi.ts";
import {ChangeLanguageRequest} from "@/app/dataModels/changeLanguageRequest.ts";

const userResources = "user";

export const changeUserLanguage = (request: ChangeLanguageRequest) => {
    return httpPost({
        url: `${userResources}/change-language`,
        body: request
    })
}