import {TextGenerationRequest} from "@/app/dataModels/languageModel/textGenerationRequest";
import {httpPost} from "@/app/core/requestApi";
import {SendMessageRequest} from "@/app/dataModels/languageModel/sendMessageRequest";
import {CreateAssistantResponse} from "@/app/dataModels/languageModel/createAssistantResponse";

const resources = 'LanguageModel';

export const correctText = async (textGenerationRequest: TextGenerationRequest)
    : Promise<{ isCorrected: boolean, correctedText: string }> => {
    return httpPost({
        url: `${resources}/correct-text`,
        body: textGenerationRequest,
    });
}

export const createAssistant = async (): Promise<CreateAssistantResponse> => {
    return httpPost({
        url: `${resources}/create-assistant`
    })
}

export const sendMessage = async (request: SendMessageRequest): Promise<string> => {
    return httpPost({
        url: `${resources}/send-message`,
        body: request,
    })
}