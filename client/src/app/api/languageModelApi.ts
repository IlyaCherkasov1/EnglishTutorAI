import {TextGenerationRequest} from "@/app/dataModels/languageModel/textGenerationRequest.ts";
import {httpPost} from "@/app/infrastructure/requestApi.ts";
import {SendMessageRequest} from "@/app/dataModels/languageModel/sendMessageRequest.ts";
import {TextCorrectionResult} from "@/app/dataModels/languageModel/textCorrectionResult.ts";

const resources = 'LanguageModel';

export const correctText = async (textGenerationRequest: TextGenerationRequest)
    : Promise<TextCorrectionResult> => {
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

export const sendMessageWithSave = async (request: SendMessageRequest): Promise<string> => {
    return httpPost({
        url: `${resources}/send-message-with-save`,
        body: request,
    })
}