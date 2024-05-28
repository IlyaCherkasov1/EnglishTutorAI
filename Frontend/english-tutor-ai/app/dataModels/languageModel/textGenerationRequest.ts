import {ThreadCreationResponse} from "@/app/dataModels/languageModel/threadCreationResponse";

export interface TextGenerationRequest extends ThreadCreationResponse {
    originalText: string,
    translatedText: string,
}