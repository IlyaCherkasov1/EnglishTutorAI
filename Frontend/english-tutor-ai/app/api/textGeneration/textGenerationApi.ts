import {RequestMethod} from "@/app/core/enum/requestMethod";
import {TextGenerationRequest} from "@/app/dataModels/textGeneration/textGenerationRequest";

export const generateChatCompletion = async (textGenerationRequest: TextGenerationRequest) => {
    const response = await fetch('https://localhost:7008/api/TextGeneration/generate-chat-completion', {
        method: RequestMethod.POST,
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(textGenerationRequest)
    })

    return response.text();
}