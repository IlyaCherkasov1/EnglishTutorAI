'use client'

import React, {useEffect, useRef, useState} from "react";
import {useI18n} from "@/app/locales/client";
import {Button} from "@/app/components/ui/button";
import DocumentCorrectionOutput from "@/app/components/component/document/documentCorrectionOutput";
import {Textarea} from "@/app/components/ui/textarea";
import {handleCorrection} from "@/app/actions/actions";
import ChatBotToggle from "@/app/components/component/chatBotToggle";
import {createAssistant} from "@/app/api/languageModel/languageModelApi";
import {CreateAssistantResponse} from "@/app/dataModels/languageModel/createAssistantResponse";

interface Props {
    documentTitle: string;
    sentences: string[];
}

export function DocumentDetail(props: Props) {
    const [currentLine, setCurrentLine] = useState(0);
    const [correctedText, setCorrectedText] = useState('');
    const [isCorrected, setIsCorrected] = useState(false);
    const [isDisplayResponse, setIsDisplayResponse] = useState(false);
    const [createAssistantResponse, setCreateAssistantResponse] = useState<CreateAssistantResponse>();

    useEffect(() => {
        createAssistant()
            .then(setCreateAssistantResponse)
            .catch(console.error);
    }, []);

    const t = useI18n()
    const formRef = useRef<HTMLFormElement>(null);

    const handleFormAction = async (formData: FormData) => {
        if (!createAssistantResponse) {
            return;
        }

        const { isCorrected, correctedText } = await handleCorrection({
            formData,
            currentLine: props.sentences[currentLine],
            createAssistantResponse,
        });

        if (isCorrected) {
            setCorrectedText(correctedText);
        } else {
            setCurrentLine((prevLine) => prevLine + 1)
            formRef.current?.reset();
        }

        setIsCorrected(isCorrected);
        setIsDisplayResponse(true);
    }

    return (
        <div>
            <div className="flex-1 overflow-auto">
                <div className="max-w-4xl mx-auto p-4">
                    <h1 className="text-3xl font-bold mb-6">{props.documentTitle}</h1>
                    <div className="bg-gray-100 p-4 rounded-md mb-4">
                        {props.sentences.map((line, index) => (
                            <span
                                key={index}
                                className={`mr-1 ${index === currentLine ? 'text-black' : 'text-gray-400'}`}>
                                {line}
                            </span>
                        ))}
                    </div>
                    <form ref={formRef} className="flex flex-col  mb-4" action={handleFormAction}>
                        <Textarea name="textarea-value" placeholder={t('enterYourText')}/>
                        <div className="flex justify-end mt-2">
                            <Button type="submit">SEND</Button>
                        </div>
                    </form>
                    {isDisplayResponse ?
                        <DocumentCorrectionOutput correctedText={correctedText} isCorrected={isCorrected}/> : <></>
                    }
                </div>
                {createAssistantResponse ?
                    <ChatBotToggle createAssistantResponse={createAssistantResponse}/> : <></>
                }
            </div>
        </div>
    )
}