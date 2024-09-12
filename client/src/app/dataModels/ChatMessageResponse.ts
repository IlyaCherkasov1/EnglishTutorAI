import {ConversationRole} from "./enums/conversationRole.ts";

export interface ChatMessageResponse {
    id: string;
    conversationRole: ConversationRole;
    content: string;
}