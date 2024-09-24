import {DocumentResponse} from "@/app/dataModels/document/documentResponse.ts";
import {useMemo, useState} from "react";
import {useTranslation} from "react-i18next";
import {saveCurrentLine} from "@/app/api/document/documentApi.ts";
import {Button} from "@/app/components/ui/button.tsx";
import {Textarea} from "@/app/components/ui/textarea.tsx";
import DocumentCorrectionOutput from "@/app/components/component/document/documentCorrectionOutput.tsx";
import {useForm} from "react-hook-form";
import {correctText} from "@/app/api/languageModel/languageModelApi.ts";
import ChatBotToggle from "@/app/components/component/chatBotToggle.tsx";

interface Props {
    document: DocumentResponse;
    sentences: string[];
}

export function DocumentDetail(props: Props) {
    const [currentLine, setCurrentLine] = useState(props.document.currentLine | 0);
    const [correctedText, setCorrectedText] = useState('');
    const [translatedText] = useState('');
    const [isCorrected, setIsCorrected] = useState(false);
    const [isDisplayCorrectionOutput, setIsDisplayCorrectionOutput] = useState(false);

    const IsDocumentFinished = useMemo(() => {
        return currentLine >= props.sentences.length;
    }, [currentLine, props.sentences.length]);

    const { t } = useTranslation();

    const { register, handleSubmit, reset, formState: { isSubmitting } } = useForm({
        defaultValues: {
            translatedText: ''
        }
    });

    const onSubmit = async (data: { translatedText: string }) => {
        const { translatedText } = data;

        const { isCorrected, correctedText } = await correctText({
            originalText: translatedText,
            translatedText: props.sentences[currentLine],
            threadId: props.document.threadId,
        })

        setIsCorrected(isCorrected);
        setIsDisplayCorrectionOutput(true);

        if (isCorrected) {
            setCorrectedText(correctedText);
        } else {
            await moveToNextLine();
        }
    }

    const moveToNextLine = async () => {
        setCurrentLine((prevLine) => prevLine + 1);
        await saveCurrentLine({ currentLine: currentLine + 1, documentId: props.document.id });
        reset();

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
                        (
                            <form className="flex flex-col mb-4" onSubmit={handleSubmit(onSubmit)}>
                                <Textarea
                                    {...register('translatedText')}
                                    name="textarea-value"
                                    placeholder={t('enterYourText')}
                                    disabled={isSubmitting}
                                />
                                <div className="flex justify-end mt-2">
                                    <Button type="submit" disabled={isSubmitting}>{t('send')}</Button>
                                </div>
                                {isSubmitting && <div
                                    className="text-center mt-4">{t('loading')}...</div>} {/* Показать индикатор загрузки */}
                            </form>
                )}
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