import {CreateAssistantResponse} from "@/app/dataModels/languageModel/createAssistantResponse";

export interface SendMessageRequest extends CreateAssistantResponse{
    message: string;
}