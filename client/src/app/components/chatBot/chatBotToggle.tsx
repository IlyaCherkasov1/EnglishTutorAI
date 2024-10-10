import {useTranslation} from "react-i18next";
import {useState} from "react";
import {Button} from "@/app/components/ui/button.tsx";
import {MessageCircle} from "lucide-react";
import {ChatBot} from "@/app/components/chatBot/chatBot.tsx";

export interface Props {
    threadId: string;
}

const ChatBotToggle = (props: Props) => {
    const { t } = useTranslation();
    const [isChatBotVisible, setIsChatBotVisible] = useState(false);

    const handleButtonClick = () => {
        setIsChatBotVisible(!isChatBotVisible);
    };

    return (
        <>
            {isChatBotVisible && <ChatBot threadId={props.threadId} />})
            <footer className="fixed p-4 bottom-0 left-0 right-0 bg-white shadow">
                <Button className="rounded-full px-4 py-2" onClick={handleButtonClick} variant="secondary">
                    <MessageCircle className="mr-2 h-5 w-5"/>
                    {t('askAI')}
                </Button>
            </footer>
        </>
    )
};

export default ChatBotToggle;