import {ConversationRole} from "@/app/dataModels/enums/conversationRole.ts";
import Markdown from "react-markdown";
import {Message} from "./ChatMessageType";
import {RefObject} from "react";

interface Props {
    messages: Message[];
    assistantTyping: boolean;
    assistantTypingMessage: string;
    scrollRef: RefObject<HTMLDivElement>;
    isProcessingResponse: boolean;
}

export const ChatMessages = (props: Props) => {
    return (
        <div className="flex flex-col gap-4">
            {props.messages.map((message, index) => (
                <div
                    key={index}
                    className={`relative p-4 flex items-start ${
                        message.sender === ConversationRole.Assistant
                            ? 'ml-3' : 'self-end bg-gray-100 rounded-3xl'}`}>
                    {message.sender === ConversationRole.Assistant && (
                        <img src="/chatgpt.png"
                             className="absolute -left-6 top-4 h-5 w-5 text-gray-500"
                             alt="bot" />
                    )}
                    <Markdown className="text-sm break-words">{message.text}</Markdown>
                </div>
            ))}
            {(props.isProcessingResponse || props.assistantTyping) && (
                <div className="relative p-4 flex items-start ml-3">
                    <img
                        src="/chatgpt.png"
                        className="absolute -left-6 top-4 h-5 w-5 text-gray-500"
                        alt="bot" />
                    {props.assistantTyping ? (
                        <Markdown className="text-sm">{props.assistantTypingMessage}</Markdown>
                    ) : (
                        <div className="w-2 h-2 rounded-full bg-black animate-ping"></div>
                    )}
                </div>
            )}
            <div ref={props.scrollRef} />
        </div>
    )
};