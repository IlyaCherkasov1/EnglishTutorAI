import {CreateAssistantResponse} from "@/app/dataModels/languageModel/createAssistantResponse";

export interface TextGenerationRequest extends CreateAssistantResponse {
    originalText: string,
    translatedText: string,
}