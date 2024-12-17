import {useLayoutEffect, useRef, useState} from "react";
import {ConversationRole} from "@/app/dataModels/enums/conversationRole.ts";
import {sendMessageWithSave} from "@/app/api/languageModelApi.ts";
import {HubConnectionBuilder} from "@microsoft/signalr";
import useAsyncEffect from "use-async-effect";
import {ChatHeader} from "@/app/components/chatBot/chatHeader.tsx";
import {Message} from "./ChatMessageType";
import {ChatMessages} from "@/app/components/chatBot/ChatMessages.tsx";
import {ChatInput} from "@/app/components/chatBot/ChatInput.tsx";
import {ChatWelcomeMessage} from "@/app/components/chatBot/chatWelcomeMessage.tsx";
import {getConversationThread} from "@/app/api/translateApi.ts";

interface Props {
    threadId: string;
    userTranslateId: string;
    closeChat: () => void;
}

export const ChatBot = (props: Props) => {
    const [messages, setMessages] = useState<Message[] | null>(null);
    const [assistantTypingMessage, setAssistantTypingMessage] = useState<string>("");
    const [assistantTyping, setAssistantTyping] = useState<boolean>(false);
    const [isProcessingResponse, setIsProcessingResponse] = useState<boolean>(false);
    const scrollRef = useRef<HTMLDivElement>(null);
    const connection = new HubConnectionBuilder()
        .withUrl('https://localhost:7008/assistantHub')
        .build();

    useAsyncEffect(async () => {
        const response = await getConversationThread(props.threadId);
        const initialMessages = response.map((chatMessage: any) => ({
            sender: chatMessage.conversationRole,
            text: chatMessage.content,
        }));

        setMessages(initialMessages);

        if (import.meta.hot) {
            import.meta.hot.accept(() => {
                setMessages([]);
            });
        }
    }, []);

    useAsyncEffect(async () => {
        await connection.start();
        await connection.invoke('JoinAssistantChat', props.threadId, props.userTranslateId);

        connection.on('ReceiveMessage', (message: string) => {
            setAssistantTyping(true);
            setAssistantTypingMessage(prev => prev + message);
            setIsProcessingResponse(false);
        });

        return () => {
            connection.stop();
        };
    }, []);

    useLayoutEffect(() => {
        if (scrollRef.current) {
            scrollRef.current.scrollIntoView({ behavior: "instant", block: "end" });
        }
    }, [messages, assistantTypingMessage, isProcessingResponse]);

    const onSubmit = async (message: string) => {
        const userMessage: Message = {
            sender: ConversationRole.User,
            text: message,
        };

        setMessages(prevMessages => [...prevMessages || [], userMessage]);
        setIsProcessingResponse(true);

        const assistantResponse = await sendMessageWithSave({
            message: message,
            threadId: props.threadId,
            groupId: `${props.userTranslateId}-${props.threadId}-Assistant`,
            userTranslateId: props.userTranslateId,
        });

        const assistantMessage: Message = {
            sender: ConversationRole.Assistant,
            text: assistantResponse,
        };

        setAssistantTyping(false);
        setMessages((prevMessages) => [...prevMessages || [], assistantMessage]);
    };

    if (!messages) {
        return null;
    }

    return (
        <div className="flex flex-col h-full bg-white border-2 rounded-md shadow-lg px-2 overflow-hidden relative">
            <ChatHeader closeChat={props.closeChat} />
            <ChatWelcomeMessage messagesLength={messages.length} />
            <div className="overflow-auto p-4 mt-12 mb-14 thin-scrollbar">
                <ChatMessages
                    messages={messages}
                    assistantTypingMessage={assistantTypingMessage}
                    assistantTyping={assistantTyping}
                    isProcessingResponse={isProcessingResponse}
                    scrollRef={scrollRef}
                />
                <ChatInput onSendMessage={onSubmit} />
            </div>
        </div>
    );
};