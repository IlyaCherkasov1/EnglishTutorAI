import {ConversationRole} from "./enums/conversationRole.ts";

export interface ChatMessageResponse {
    conversationRole: ConversationRole;
    content: string;
}