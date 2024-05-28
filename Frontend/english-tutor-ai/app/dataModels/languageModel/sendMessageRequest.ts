import {ThreadCreationResponse} from "@/app/dataModels/languageModel/threadCreationResponse";

export interface SendMessageRequest extends ThreadCreationResponse{
    message: string;
}