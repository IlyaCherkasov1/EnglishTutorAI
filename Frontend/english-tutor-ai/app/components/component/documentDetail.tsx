'use client'

import React, {useRef, useState} from "react";
import {useI18n} from "@/app/locales/client";
import {generateChatCompletion} from "@/app/api/textGeneration/textGenerationApi";
import {Button} from "@/app/components/ui/button";
import DocumentCorrectionOutput from "@/app/components/component/document/documentCorrectionOutput";
import {Textarea} from "@/app/components/ui/textarea";

interface Props {
    documentTitle: string;
    sentences: string[];
}

export function DocumentDetail(props: Props) {
    const [currentLine, setCurrentLine] = useState(0);
    const [translatedText, setTranslatedText] = useState('');
    const [correctedText, setCorrectedText] = useState('');
    const [isCorrected, setIsCorrected] = useState(false);
    const t = useI18n()
    const formRef = useRef<HTMLFormElement>(null);

    const checkSentence = async (formData: FormData): Promise<void> => {
        const translatedText = formData.get('textarea-value') as string;

        const response = await generateChatCompletion({
            originalText: props.sentences[currentLine],
            translatedText: translatedText,
        })

        setTranslatedText(translatedText);
        setStates(response);
    }

    const setStates = (response: string) => {
        setCorrectedText(response);
        setCurrentLine((prevLine) => prevLine + 1)
        setIsCorrected(true);
    };

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
                    <form ref={formRef} className="flex flex-col  mb-4" action={async (formData) => {
                        await checkSentence(formData);
                        formRef.current?.reset();
                    }}>
                        <Textarea name="textarea-value" placeholder={t('enterYourText')}/>
                        <div className="flex justify-end mt-2">
                            <Button type="submit">SEND</Button>
                        </div>
                    </form>
                    {isCorrected ? (
                        <DocumentCorrectionOutput correctedText={correctedText} translatedText={translatedText}/>
                    ) : (<> </>)
                    }
                </div>
            </div>
        </div>
    )
}