import {DocumentResponse} from "@/app/dataModels/document/documentResponse.ts";
import React, {useMemo, useState} from "react";
import {useTranslation} from "react-i18next";
import {getConversationThread, saveCurrentLine} from "@/app/api/documentApi.ts";
import {Button} from "@/app/components/ui/button.tsx";
import {Textarea} from "@/app/components/ui/textarea.tsx";
import DocumentCorrectionOutput from "@/app/components/document/documentCorrectionOutput.tsx";
import {FormProvider, useForm} from "react-hook-form";
import {correctText} from "@/app/api/languageModelApi.ts";
import ChatBotToggle from "@/app/components/chatBot/chatBotToggle.tsx";
import {zodResolver} from "@hookform/resolvers/zod";
import {FormMessage} from "@/app/components/ui/form.tsx";
import {
    DocumentDetailsSchema,
    TDocumentDetailsSchema
} from "@/app/infrastructure/validationSchemas/documentDetailsSchema.ts";
import {ChatMessageResponse} from "@/app/dataModels/chatMessageResponse.ts";
import useAsyncEffect from "use-async-effect";
import {LoadingSpinner} from "@/app/components/ui/loadingSpinner.tsx";
import {DocumentResult} from "@/app/components/document/documentResult.tsx";
import {restartDocumentSession} from "@/app/api/documentSessionApi.ts";

interface Props {
    document: DocumentResponse;
}

export const DocumentDetail = (props: Props) => {
    const [currentLine, setCurrentLine] = useState(props.document.currentLine);
    const [correctedText, setCorrectedText] = useState('');
    const [translatedText, setTranslatedText] = useState('');
    const [chatMessageResponse, setChatMessageResponse] = useState<ChatMessageResponse[]>([]);
    const [isCorrected, setIsCorrected] = useState(false);
    const [sessionId, setSessionId] = useState<string>(props.document.sessionId);

    const { t } = useTranslation();

    useAsyncEffect(async () => {
        const response = await getConversationThread(props.document.threadId);
        setChatMessageResponse(response);
    }, [props.document.threadId]);

    const methods = useForm<TDocumentDetailsSchema>({ resolver: zodResolver(DocumentDetailsSchema) });
    const { register, handleSubmit, reset, setFocus, formState: { errors, isSubmitting } } = methods;

    const isDocumentFinished = useMemo(() => {
        return currentLine >= props.document.sentences.length;
    }, [currentLine, props.document.sentences.length]);

    const onSubmit = async (data: { translatedText: string }) => {
        const { translatedText } = data;

        const result = await correctText({
            originalText: props.document.sentences[currentLine],
            translatedText: translatedText,
            threadId: props.document.threadId,
            documentId: props.document.id,
            sessionId: sessionId,
        })

        setTranslatedText(translatedText);
        setIsCorrected(result.isCorrected);

        if (result.isCorrected) {
            setCorrectedText(result.correctedText);
        } else {
            await moveToNextLine();
        }

        setFocus("translatedText");
    }

    const moveToNextLine = async () => {
        setCurrentLine((prevLine) => prevLine + 1);
        await saveCurrentLine({ currentLine: currentLine + 1, documentId: props.document.id });

        reset();
        setFocus("translatedText");
    };

    const handleStartAgain = async () => {
        await saveCurrentLine({ currentLine: 0, documentId: props.document.id });
        const newSessionId = await restartDocumentSession({
            documentId: props.document.id,
            currentSessionId: props.document.sessionId,
        })

        setSessionId(newSessionId);
        setCurrentLine(0);
        setTranslatedText('');
    }

    const handleKeyDown = (event: React.KeyboardEvent<HTMLTextAreaElement>) => {
        if (event.key === 'Enter' && !event.shiftKey) {
            event.preventDefault();
            handleSubmit(onSubmit)();
        }
    };

    return (
        <div>
            <div className="max-w-4xl mx-auto p-4">
                <h1 className="text-3xl font-bold mb-6">{props.document.title}</h1>
                <div className="bg-gray-100 p-4 rounded-md mb-4">
                    {props.document.sentences.map((line, index) => (
                        <span
                            key={index}
                            className={`mr-1 ${index === currentLine ? 'text-black' : 'text-gray-400'}`}>
                                {line}
                            </span>
                    ))}
                </div>
                {isDocumentFinished ? (
                    <>
                        <div className="flex justify-end mt-2">
                            <Button onClick={handleStartAgain}>{t('startAgain')}</Button>
                        </div>
                        <DocumentResult sessionId={sessionId} />
                    </>
                ) : (
                    <div>
                        <FormProvider {...methods}>
                            <form onSubmit={handleSubmit(onSubmit)} className="flex flex-col mb-4">
                                <Textarea
                                    {...register('translatedText', { required: true })
                                    }
                                    placeholder={t('enterYourText')}
                                    disabled={isSubmitting}
                                    onKeyDown={handleKeyDown} />
                                <FormMessage>{errors.translatedText?.message}</FormMessage>
                                <div className="flex justify-end mt-2">
                                    <Button type="submit" disabled={isSubmitting}>{t('send')}</Button>
                                </div>
                                {isSubmitting && (
                                    <div className="flex items-center justify-center mt-2">
                                        <LoadingSpinner />
                                    </div>
                                )}
                            </form>
                        </FormProvider>
                        {translatedText.length > 0 &&
                            <DocumentCorrectionOutput
                                translatedText={translatedText}
                                correctedText={correctedText}
                                isCorrected={isCorrected}
                                threadId={props.document.threadId}
                                currentLine={currentLine}
                            />
                        }
                    </div>
                )}
            </div>
            <ChatBotToggle chatMessageResponse={chatMessageResponse} threadId={props.document.threadId} />
        </div>
    )
}