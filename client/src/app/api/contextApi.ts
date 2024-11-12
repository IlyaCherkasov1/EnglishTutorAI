import {httpGet} from "@/app/infrastructure/requestApi.ts";
import {ContextResponse} from "@/app/dataModels/contextResponse.ts";

export const getContext = async (): Promise<ContextResponse> => {
    return httpGet({
        url: "context"
    })
}