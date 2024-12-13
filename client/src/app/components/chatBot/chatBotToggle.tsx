import {useTranslation} from "react-i18next";
import {useState} from "react";
import {Button} from "@/app/components/ui/button.tsx";
import {MessageCircle} from "lucide-react";
import {ChatBot} from "@/app/components/chatBot/chatBot.tsx";
import {ChatMessageResponse} from "@/app/dataModels/chatMessageResponse.ts";

export interface Props {
    threadId: string;
    chatMessageResponse: ChatMessageResponse[];
    userTranslateId: string;
}

const ChatBotToggle = (props: Props) => {
    const { t } = useTranslation();
    const [isChatBotVisible, setIsChatBotVisible] = useState(false);

    const handleButtonClick = () => {
        setIsChatBotVisible(!isChatBotVisible);
    };

    return (
        <div>
            {isChatBotVisible ? (
                <div className="fixed bottom-0 right-4 w-full sm:w-96 z-50 bg-white h-[90vh] mb-3">
                    <ChatBot
                        chatMessageResponse={props.chatMessageResponse}
                        threadId={props.threadId}
                        userTranslateId={props.userTranslateId}
                        closeChat={() => setIsChatBotVisible(false)}
                    />
                </div>
            ) : (
                <Button className="fixed bottom-4 right-4 p-4 rounded-full px-4 py-2" onClick={handleButtonClick}
                        variant="secondary">
                    <MessageCircle className="mr-2 h-5 w-5" />
                    {t('askAI')}
                </Button>
            )}
        </div>
    )
};

export default ChatBotToggle;