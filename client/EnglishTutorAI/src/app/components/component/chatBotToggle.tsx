import {useTranslation} from "react-i18next";
import {useState} from "react";
import {Button} from "../ui/button.tsx";
import {MessageCircle} from "lucide-react";
import {ChatBot} from "./chatBot.tsx";

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
            <div className="p-4 bg-white shadow fixed bottom-0 left-0 right-0">
                <Button className="rounded-full px-4 py-2" onClick={handleButtonClick} variant="secondary">
                    <MessageCircle className="mr-2 h-5 w-5"/>
                    {t('askAI')}
                </Button>
            </div>
            {isChatBotVisible && <ChatBot threadId={props.threadId} />})
        </>
    )
};

export default ChatBotToggle;