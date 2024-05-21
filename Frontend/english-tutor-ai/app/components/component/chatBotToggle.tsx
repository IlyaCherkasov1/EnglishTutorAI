import React from 'react';
import {Button} from "@/app/components/ui/button";
import {MessageCircle} from "lucide-react";
import {useI18n} from "@/app/locales/client";

const ChatBotToggle = () => {
    const t = useI18n()

    return (
        <>
            <div className="p-4 bg-white shadow fixed bottom-0 left-0 right-0">
                <Button className="rounded-full px-4 py-2" variant="secondary">
                    <MessageCircle className="mr-2 h-5 w-5"/>
                    {t('askAI')}
                </Button>
            </div>
        </>
    )
};

export default ChatBotToggle;