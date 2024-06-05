'use client'

import React, {useMemo, useRef, useState} from "react";
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
    const [translatedText, setTranslatedText] = useState('');
    const [isCorrected, setIsCorrected] = useState(false);
    const [isDisplayCorrectionOutput, setIsDisplayCorrectionOutput] = useState(false);

    const IsDocumentFinished = useMemo(() => {
        return currentLine >= props.sentences.length;
    }, [currentLine, props.sentences.length]);

    const t = useI18n()
    const formRef = useRef<HTMLFormElement>(null);

    const handleFormAction = async (formData: FormData) => {
        const translatedText = formData.get('textarea-value') as string;
        setTranslatedText(translatedText);

        const { isCorrected, correctedText } = await handleCorrection({
            translatedText,
            currentLine: props.sentences[currentLine],
            threadId: props.document.threadId,
        });

        setIsCorrected(isCorrected);
        setIsDisplayCorrectionOutput(true);
        isCorrected ? setCorrectedText(correctedText) : await moveToNextLine();
    }

    const moveToNextLine = async () => {
        setCurrentLine((prevLine) => prevLine + 1);
        await saveCurrentLine({ currentLine: currentLine + 1, documentId: props.document.id });
        formRef.current?.reset();

        if (currentLine >= props.sentences.length - 1) {
            setIsDisplayCorrectionOutput(false);
        }
    };

    const handleStartAgain = async () => {
        await saveCurrentLine({ currentLine: 0, documentId: props.document.id });
        setCurrentLine(0);
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
                    {IsDocumentFinished ? (
                            <div className="flex justify-end mt-2">
                                <Button onClick={handleStartAgain}>{t('startAgain')}</Button>
                            </div>
                        ) :
                        (<form ref={formRef} className="flex flex-col mb-4" action={handleFormAction}>
                            <Textarea name="textarea-value" placeholder={t('enterYourText')}/>
                            <div className="flex justify-end mt-2">
                                <Button type="submit">{t('send')}</Button>
                            </div>
                        </form>)}
                    {isDisplayCorrectionOutput ?
                        <DocumentCorrectionOutput
                            originalText={translatedText}
                            correctedText={correctedText}
                            isCorrected={isCorrected}/>
                        : null
                    }
                </div>
                <ChatBotToggle threadId={props.document.threadId}/>
            </div>
        </div>
    )
}