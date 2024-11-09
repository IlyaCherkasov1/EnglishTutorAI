import {httpPost} from "@/app/infrastructure/requestApi.ts";
import {RestartDocumentSessionRequest} from "@/app/dataModels/document/restartDocumentSessionRequest.ts";

const documentSessionResource = "documentSession";

export const restartDocumentSession = async (request: RestartDocumentSessionRequest): Promise<string> => {
    return httpPost({
        url: `${documentSessionResource}/restart-document-session`,
        body: request,
    })
}