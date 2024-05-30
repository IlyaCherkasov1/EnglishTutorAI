import {ConversationRole} from "@/app/dataModels/enums/conversationRole";

export interface ChatMessageResponse {
    id: string;
    conversationRole: ConversationRole;
    content: string;
}