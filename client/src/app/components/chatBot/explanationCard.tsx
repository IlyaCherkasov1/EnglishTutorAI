import {Accordion, AccordionContent, AccordionItem, AccordionTrigger} from "@/app/components/ui/accordion.tsx";
import {sendMessage} from "@/app/api/languageModelApi.ts";
import {useState} from "react";
import {HubConnectionBuilder} from "@microsoft/signalr";
import useAsyncEffect from "use-async-effect";
import Markdown from "react-markdown";
import {useTranslation} from "react-i18next";
import {SubmitSpinner} from "@/app/components/ui/submitSpinner.svg.tsx";

interface Props {
    threadId: string;
    currentLine: number;
    isCorrected: boolean;
    userTranslateId: string;
}

export const ExplanationCard = (props: Props) => {
    const [assistantTypingMessage, setAssistantTypingMessage] = useState<string>("");
    const [isLoading, setIsLoading] = useState<boolean>(false);
    const { t } = useTranslation();
    const connection = new HubConnectionBuilder()
        .withUrl(import.meta.env.VITE_APP_ASSISTANT_HUB)
        .build();

    useAsyncEffect(async () => {
        await connection.start();
        await connection.invoke('JoinExplanationChat', props.threadId, props.userTranslateId);

        connection.on('ReceiveMessage', (message: string) => {
            setAssistantTypingMessage(prev => prev + message);
            setIsLoading(false);
        });

        return () => {
            connection.stop();
        };
    }, []);

    const getLocalStorageKey = (threadId: string, currentLine: number) => {
        return `${threadId}-line-${currentLine}`;
    }

    const clearOldCache = (threadId: string) => {
        const keys = Object.keys(localStorage);
        keys.forEach(key => {
            if (key.startsWith(`${threadId}-line-`)) {
                localStorage.removeItem(key);
            }
        });
    }

    const accordionTriggerHandler = async () => {
        const localStorageKey = getLocalStorageKey(props.threadId, props.currentLine);
        const cachedMessage = localStorage.getItem(localStorageKey);

        if (cachedMessage) {
            setAssistantTypingMessage(cachedMessage);
        } else {
            setIsLoading(true);

            const response = await sendMessage({
                message: t('explanationCardCommand'),
                threadId: props.threadId,
                groupId: `${props.userTranslateId}-${props.threadId}-Explanation`,
                userTranslateId: props.userTranslateId
            });

            clearOldCache(props.threadId);
            localStorage.setItem(localStorageKey, response);
            setAssistantTypingMessage(response);
        }
    }

    return (
        <Accordion type="single" collapsible>
            <AccordionItem value="item-1">
                <AccordionTrigger className="text-lg" onClick={accordionTriggerHandler}>{t('explanation')}
                </AccordionTrigger>
                <AccordionContent>
                    {isLoading ? (
                        <div className="flex justify-center">
                            <SubmitSpinner />
                        </div>
                    ) : (
                        <Markdown className="text-sm">{assistantTypingMessage}</Markdown>
                    )}
                </AccordionContent>
            </AccordionItem>
        </Accordion>
    )
}