'use client'

import React, {useRef, useState} from "react";
import {useI18n} from "@/app/locales/client";
import {Button} from "@/app/components/ui/button";
import DocumentCorrectionOutput from "@/app/components/component/document/documentCorrectionOutput";
import {Textarea} from "@/app/components/ui/textarea";
import {handleCorrection} from "@/app/actions/actions";
import ChatBotToggle from "@/app/components/component/chatBotToggle";
import {saveCurrentLine} from "@/app/api/document/documentApi";
import {DocumentResponse} from "@/app/dataModels/document/documentResponse";

interface Props {
    document: DocumentResponse;
    sentences: string[];
}

export function DocumentDetail(props: Props) {
    const [currentLine, setCurrentLine] = useState(props.document.currentLine | 0);
    const [correctedText, setCorrectedText] = useState('');
    const [isCorrected, setIsCorrected] = useState(false);
    const [isDisplayResponse, setIsDisplayResponse] = useState(false);

    const t = useI18n()
    const formRef = useRef<HTMLFormElement>(null);

    const handleFormAction = async (formData: FormData) => {
        const { isCorrected, correctedText } = await handleCorrection({
            formData,
            currentLine: props.sentences[currentLine],
            threadId: props.document.threadId,
        });

        if (isCorrected) {
            setCorrectedText(correctedText);
        } else {
            setCurrentLine((prevLine) => prevLine + 1)
            await saveCurrentLine({ currentLine: currentLine + 1, documentId: props.document.id });
            formRef.current?.reset();
        }

        setIsCorrected(isCorrected);
        setIsDisplayResponse(true);
    }

    return (
        <div>
            <div className="flex-1 overflow-auto">
                <div className="max-w-4xl mx-auto p-4">
                    <h1 className="text-3xl font-bold mb-6">{props.document.title}</h1>
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
                <ChatBotToggle threadId={props.document.threadId}/>
            </div>
        </div>
    )
}