import {ConversationRole} from "@/app/dataModels/enums/conversationRole.ts";

export type Message = {
    sender: ConversationRole;
    text: string;
};