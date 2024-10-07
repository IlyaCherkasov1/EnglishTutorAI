import {useEffect, useState} from "react";
import {ChatMessageResponse} from "@/app/dataModels/chatMessageResponse.ts";
import {getConversationThread} from "@/app/api/document/documentApi.ts";
import {ConversationRole} from "@/app/dataModels/enums/conversationRole.ts";
import {sendMessage} from "@/app/api/languageModel/languageModelApi.ts";
import {Bot} from "lucide-react";
import {Input} from "@/app/components/ui/input.tsx";
import {Button} from "@/app/components/ui/button.tsx";
import {useTranslation} from "react-i18next";
import {useForm} from "react-hook-form";
import useAsyncEffect from "use-async-effect";

interface Props {
    threadId: string;
}

type Message = {
    sender: ConversationRole,
    text: string,
};

type FormValues = {
    message: string;
};

export const ChatBot = (props: Props) => {
    const [messages, setMessages] = useState<Message[]>([]);
    const [chatMessageResponse, setChatMessageResponse] = useState<ChatMessageResponse[]>();
    const { t } = useTranslation();
    const { register, handleSubmit, reset, formState: { isSubmitting } } = useForm<FormValues>();

    useAsyncEffect(async () => {
        const response = await getConversationThread(props.threadId);
        setChatMessageResponse(response);
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

    const onSubmit = async (data: FormValues) => {
        const userMessage: Message = {
            sender: ConversationRole.User,
            text: data.message,
        };
        setMessages([...messages, userMessage]);

        const assistantResponse = await sendMessage({
            message: data.message,
            threadId: props.threadId,
        });

        const botMessage: Message = {
            sender: ConversationRole.Assistant,
            text: assistantResponse,
        };
        setMessages(prevMessages => [...prevMessages, botMessage]);
        reset();
    };

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
                                             message.sender === ConversationRole.Assistant
                                                 ? 'ml-3' : 'self-end bg-gray-100 rounded-3xl'}`}>
                                        {message.sender === ConversationRole.Assistant &&
                                            <Bot className="absolute -left-6 top-4 h-5 w-5 text-gray-500" />}
                                        <p className="text-sm">{message.text}</p>
                                    </div>
                                ))}
                            </div>
                        </div>
                        <form onSubmit={handleSubmit(onSubmit)}
                              className="p-4 bg-white shadow border-t flex items-center justify-between">
                            <div className="flex items-center w-full space-x-2">
                                <Input
                                    className="flex-1 rounded-full px-4 py-2"
                                    placeholder={t('writeYourMessage')}
                                    type="text"
                                    {...register("message", { required: true })}
                                    disabled={isSubmitting}
                                />
                                <Button type="submit" disabled={isSubmitting}>
                                    {isSubmitting ? t('sending...') : t('send')}
                                </Button>
                            </div>
                        </form>
                        {isSubmitting &&
                            <div className="text-center mt-4">{t('loading')}...</div>} {/* Show loading indicator */}
                    </div>
                </div>
            </main>
        </div>
    );
};