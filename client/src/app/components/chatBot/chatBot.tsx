import {useEffect, useState} from "react";
import {ChatMessageResponse} from "@/app/dataModels/chatMessageResponse.ts";
import {ConversationRole} from "@/app/dataModels/enums/conversationRole.ts";
import {sendMessageWithSave} from "@/app/api/languageModelApi.ts";
import {MoveUp, X} from "lucide-react";
import {Button} from "@/app/components/ui/button.tsx";
import {useTranslation} from "react-i18next";
import {useForm} from "react-hook-form";
import {HubConnectionBuilder} from "@microsoft/signalr";
import useAsyncEffect from "use-async-effect";
import Markdown from "react-markdown";
import {Textarea} from "@/app/components/ui/textarea.tsx";

interface Props {
    threadId: string;
    chatMessageResponse: ChatMessageResponse[];
    userTranslateId: string;
    closeChat: () => void;
}

type Message = {
    sender: ConversationRole;
    text: string;
};

type FormValues = {
    message: string;
};

export const ChatBot = (props: Props) => {
    const [messages, setMessages] = useState<Message[]>([]);
    const [assistantTypingMessage, setAssistantTypingMessage] = useState<string>("");
    const [assistantTyping, setAssistantTyping] = useState<boolean>(false);
    const [isProcessingResponse, setIsProcessingResponse] = useState<boolean>(false);
    const { t } = useTranslation();
    const { register, handleSubmit, reset, formState: { isSubmitting } } = useForm<FormValues>();
    const connection = new HubConnectionBuilder()
        .withUrl('https://localhost:7008/assistantHub')
        .build();

    useEffect(() => {
        if (props.chatMessageResponse.length > 0) {
            const newMessages = props.chatMessageResponse.map((chatMessage) => ({
                sender: chatMessage.conversationRole,
                text: chatMessage.content,
            }));

            setMessages((prevMessages) => [...prevMessages, ...newMessages]);
        }

        if (import.meta.hot) {
            import.meta.hot.accept(() => {
                setMessages([]);
            });
        }

    }, [props.chatMessageResponse]);

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

    const onSubmit = async (data: FormValues) => {
        const userMessage: Message = {
            sender: ConversationRole.User,
            text: data.message,
        };

        setMessages(prevMessages => [...prevMessages, userMessage]);
        setIsProcessingResponse(true);
        reset();

        const assistantResponse = await sendMessageWithSave({
            message: data.message,
            threadId: props.threadId,
            groupId: `${props.userTranslateId}-${props.threadId}-Assistant`,
            userTranslateId: props.userTranslateId,
        });

        const assistantMessage: Message = {
            sender: ConversationRole.Assistant,
            text: assistantResponse,
        };

        setAssistantTyping(false);
        setMessages((prevMessages) => [...prevMessages, assistantMessage]);
    };

    return (
        <div className="flex flex-col h-full bg-white border-2 rounded-md shadow-lg px-2">
            <div className="absolute top-2 right-2">
                <Button
                    onClick={props.closeChat}
                    className="p-2 bg-white hover:bg-gray-300"
                    variant="secondary">
                    <X className="h-5 w-5 text-gray-600" />
                </Button>
            </div>

            {messages.length === 0 && (
                <div
                    className="flex justify-center items-center text-xl text-gray-500 flex-1">
                    How can I help with your translation?
                </div>
            )}

            <div className="overflow-auto p-4 mt-10 scrollbar-opacity">
                <div className="flex flex-col gap-4">
                    {messages.map((message, index) => (
                        <div
                            key={index}
                            className={`relative p-4 flex items-start ${
                                message.sender === ConversationRole.Assistant
                                    ? 'ml-3' : 'self-end bg-gray-100 rounded-3xl'}`}>
                            {message.sender === ConversationRole.Assistant && (
                                <img src="/public/chatgpt.png"
                                     className="absolute -left-6 top-4 h-5 w-5 text-gray-500"
                                     alt="bot" />
                            )}
                            <Markdown className="text-sm">{message.text}</Markdown>
                        </div>
                    ))}
                    {(isProcessingResponse || assistantTyping) && (
                        <div className="relative p-4 flex items-start ml-3">
                            <img src="/public/chatgpt.png"
                                 className="absolute -left-6 top-4 h-5 w-5 text-gray-500"
                                 alt="bot" />
                            {assistantTyping ? (
                                <Markdown className="text-sm">{assistantTypingMessage}</Markdown>
                            ) : (
                                <div className="w-3 h-3 rounded-full bg-black animate-ping"></div>
                            )}
                        </div>
                    )}
                </div>
                <form onSubmit={handleSubmit(onSubmit)}>
                    <div className="flex flex-col bg-gray-200 w-full rounded-lg">
                        <Textarea
                            className="bg-gray-200 h-5 rounded-lg focus-visible:outline-none focus-visible:ring-0 shadow-none resize-none"
                            placeholder={t('writeYourMessage')}
                            {...register("message", { required: true })}
                            disabled={isSubmitting}
                        />
                        <div className="flex justify-end mr-1">
                            <div
                                className="w-9 cursor-pointer p-3 bg-black text-white rounded-full  hover:bg-gray-500 transition-all"
                                onClick={handleSubmit(onSubmit)}>
                                <MoveUp className="h-3 w-3 stroke-5"  />
                            </div>
                        </div>
                    </div>
                </form>
            </div>
        </div>
    );
};