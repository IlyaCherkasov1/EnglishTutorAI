import {TextGenerationRequest} from "@/app/dataModels/languageModel/textGenerationRequest";
import {httpPost} from "@/app/core/requestApi";
import {SendMessageRequest} from "@/app/dataModels/languageModel/sendMessageRequest";
import {ThreadCreationResponse} from "@/app/dataModels/languageModel/threadCreationResponse";

const resources = 'LanguageModel';

export const correctText = async (textGenerationRequest: TextGenerationRequest)
    : Promise<{ isCorrected: boolean, correctedText: string }> => {
    return httpPost({
        url: `${resources}/correct-text`,
        body: textGenerationRequest,
    });
}

export const createThread = async (documentId: string): Promise<ThreadCreationResponse> => {
    return httpPost({
        url: `${resources}/create-thread/${documentId}`
    })
}

export const sendMessage = async (request: SendMessageRequest): Promise<string> => {
    return httpPost({
        url: `${resources}/send-message`,
        body: request,
    })
}