import {TextGenerationRequest} from "@/app/dataModels/textGeneration/textGenerationRequest";
import {httpPost} from "@/app/core/requestApi";

const resources = 'textGeneration';

export const generateChatCompletion = async (textGenerationRequest: TextGenerationRequest) => {
    return httpPost({
        url: `${resources}/generate-chat-completion`,
        body: textGenerationRequest,
    });
}