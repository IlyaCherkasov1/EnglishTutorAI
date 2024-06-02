import {Input} from "@/app/components/ui/input"
import {Button} from "@/app/components/ui/button"
import {ArrowUp, Bot} from "lucide-react";
import React, {useEffect, useRef, useState} from "react";
import {sendMessage} from "@/app/api/languageModel/languageModelApi";
import {getConversationThread} from "@/app/api/document/documentApi";
import {ChatMessageResponse} from "@/app/dataModels/ChatMessageResponse";
import {ConversationRole} from "@/app/dataModels/enums/conversationRole";

interface Props {
    threadId: string;
}

type Message = {
    sender: ConversationRole,
    text: string,
};

export const ChatBot = (props: Props) => {
    const [messages, setMessages] = useState<Message[]>([]);
    const [chatMessageResponse, setChatMessageResponse] = useState<ChatMessageResponse[]>();

    useEffect(() => {
        const fetchConversationThread = async () => {
            const response = await getConversationThread(props.threadId);
            setChatMessageResponse(response);
        };

        fetchConversationThread().catch(console.error);
    }, [props.threadId]);

    useEffect(() => {
        if (chatMessageResponse) {
            const newMessages = chatMessageResponse.map((chatMessage) => ({
                sender: chatMessage.conversationRole,
                text: chatMessage.content,
            }));

            setMessages((prevMessages) => [...prevMessages, ...newMessages]);
        }
    }, [chatMessageResponse]);

    const handleOnClick = async (formData: FormData) => {
        const inputValue = formData.get("message") as string;
        const userMessage: Message = {
            sender: ConversationRole.User, text: inputValue,
        };
        setMessages([...messages, userMessage]);

        const assistantResponse = await sendMessage(
            {
                message: inputValue,
                threadId: props.threadId,
            });

        const botMessage: Message = {
            sender: ConversationRole.Assistant, text: assistantResponse
        };
        setMessages(prevMessages => [...prevMessages, botMessage]);
        formRef.current?.reset();
    }

    const formRef = useRef<HTMLFormElement>(null);

    return (
        <div className="flex flex-col h-screen">
            <main className="flex-1 overflow-auto">
                <div className="max-w-4xl mx-auto p-4">
                    <div className="flex flex-col h-[600px] bg-white border-2 rounded-md shadow-lg">
                        <div className="flex-1 overflow-auto p-6">
                            <div className="flex flex-col gap-4">
                                {messages.map((message, index) => (
                                    <div key={index}
                                         className={`relative p-4 flex items-start ${
                                             message.sender === ConversationRole.Assistant ?
                                                 'ml-3' : 'self-end bg-gray-100 rounded-3xl'}`}>
                                        {message.sender === ConversationRole.Assistant &&
                                            <Bot className="absolute -left-6 top-4 h-5 w-5 text-gray-500"/>}
                                        <p className="text-sm">{message.text}</p>
                                    </div>
                                ))}
                            </div>
                        </div>
                        <form ref={formRef} action={async (formData) => {
                            await handleOnClick(formData);
                        }} className="p-4 bg-white shadow border-t flex items-center justify-between">
                            <div className="flex items-center w-full space-x-2">
                                <Input className="flex-1 rounded-full px-4 py-2" name="message"
                                       placeholder="Write your message..." type="text"/>
                                <Button onClick={() => handleOnClick} className="rounded-full px-3 py-2" type="submit"
                                        variant="secondary">
                                    <ArrowUp className="h-5 w-5"/>
                                </Button>
                            </div>
                        </form>
                    </div>
                </div>
            </main>
        </div>
    )
}