import {DocumentDetailsModel} from "@/app/dataModels/document/documentDetailsModel.ts";
import React, {useState} from "react";
import {useTranslation} from "react-i18next";
import {
    getConversationThread,
    handleDocumentCompletion,
    handleDocumentStart,
    saveCurrentLine
} from "@/app/api/documentApi.ts";
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
import {DocumentResult} from "@/app/components/document/documentResult.tsx";
import {SubmitSpinner} from "@/app/components/ui/submitSpinner.svg.tsx";

interface Props {
    documentDetails: DocumentDetailsModel;
}

export const DocumentDetail = (props: Props) => {
    const [currentLine, setCurrentLine] = useState(props.documentDetails.currentLine);
    const [correctedText, setCorrectedText] = useState('');
    const [translatedText, setTranslatedText] = useState('');
    const [chatMessageResponse, setChatMessageResponse] = useState<ChatMessageResponse[]>([]);
    const [isCorrected, setIsCorrected] = useState(false);
    const [isDocumentFinished, setIsDocumentFinished] = useState(props.documentDetails.isDocumentFinished);

    const { t } = useTranslation();

    useAsyncEffect(async () => {
        const response = await getConversationThread(props.documentDetails.threadId);
        setChatMessageResponse(response);
    }, [props.documentDetails.threadId]);

    const methods = useForm<TDocumentDetailsSchema>({ resolver: zodResolver(DocumentDetailsSchema) });
    const { register, handleSubmit, reset, setFocus, formState: { errors, isSubmitting } } = methods;

    const onSubmit = async (data: { translatedText: string }) => {
        const { translatedText } = data;

        const result = await correctText({
            originalText: props.documentDetails.sentences[currentLine],
            translatedText: translatedText,
            threadId: props.documentDetails.threadId,
            userDocumentId: props.documentDetails.userDocumentId,
        });

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
        const nextLine = currentLine + 1;
        setCurrentLine(nextLine);

        if (nextLine >= props.documentDetails.sentences.length) {
            setIsDocumentFinished(true);
            await handleDocumentCompletion(props.documentDetails.userDocumentId);
        }

        await saveCurrentLine({ currentLine: currentLine + 1, userDocumentId: props.documentDetails.userDocumentId });

        reset();
        setFocus("translatedText");
    };

    const handleStartAgain = async () => {
        await handleDocumentStart(props.documentDetails.userDocumentId);

        setIsDocumentFinished(false)
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
                <h1 className="text-3xl font-bold mb-6">{props.documentDetails.title}</h1>
                <div className="bg-gray-100 p-4 rounded-md mb-4">
                    {props.documentDetails.sentences.map((line, index) => (
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
                        <DocumentResult userDocumentId={props.documentDetails.userDocumentId} />
                    </>
                ) : (
                    <>
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
                                    <div className="mt-2">
                                        <SubmitSpinner />
                                    </div>
                                )}
                            </form>
                        </FormProvider>
                        {translatedText.length > 0 &&
                            <DocumentCorrectionOutput
                                translatedText={translatedText}
                                correctedText={correctedText}
                                isCorrected={isCorrected}
                                threadId={props.documentDetails.threadId}
                                currentLine={currentLine}
                                userDocumentId={props.documentDetails.userDocumentId}
                            />
                        }
                    </>
                )}
                <ChatBotToggle
                    chatMessageResponse={chatMessageResponse}
                    threadId={props.documentDetails.threadId}
                    userDocumentId={props.documentDetails.userDocumentId} />
            </div>
        </div>
    )
}