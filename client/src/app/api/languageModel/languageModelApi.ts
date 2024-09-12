import {TextGenerationRequest} from "../../dataModels/languageModel/textGenerationRequest.ts";
import {httpPost} from "../../infrastructure/requestApi.ts";
import {SendMessageRequest} from "../../dataModels/languageModel/sendMessageRequest.ts";

const resources = 'LanguageModel';

export const correctText = async (textGenerationRequest: TextGenerationRequest)
    : Promise<{ isCorrected: boolean, correctedText: string }> => {
    return httpPost({
        url: `${resources}/correct-text`,
        body: textGenerationRequest,
    });
}

export const sendMessage = async (request: SendMessageRequest): Promise<string> => {
    return httpPost({
        url: `${resources}/send-message`,
        body: request,
    })
}