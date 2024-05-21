import React, {useState} from 'react';
import {Button} from "@/app/components/ui/button";
import {MessageCircle} from "lucide-react";
import {ChatBot} from "@/app/components/component/chatBot";
import {useI18n} from "@/app/locales/client";

const ChatBotToggle = () => {
    const t = useI18n()
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
            {isChatBotVisible && <ChatBot/>})
        </>
    )
};

export default ChatBotToggle;