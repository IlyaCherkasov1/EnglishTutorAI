import {DocumentResponse} from "@/app/dataModels/document/documentResponse.ts";
import {useMemo, useState} from "react";
import {useTranslation} from "react-i18next";
import {getConversationThread, saveCurrentLine} from "@/app/api/document/documentApi.ts";
import {Button} from "@/app/components/ui/button.tsx";
import {Textarea} from "@/app/components/ui/textarea.tsx";
import DocumentCorrectionOutput from "@/app/components/document/documentCorrectionOutput.tsx";
import {FormProvider, useForm} from "react-hook-form";
import {correctText} from "@/app/api/languageModel/languageModelApi.ts";
import ChatBotToggle from "@/app/components/chatBot/chatBotToggle.tsx";
import {zodResolver} from "@hookform/resolvers/zod";
import {FormMessage} from "@/app/components/ui/form.tsx";
import {DocumentDetailsSchema, TDocumentDetailsSchema} from "@/app/infrastructure/validationSchemas/documentDetailsSchema.ts";
import {ChatMessageResponse} from "@/app/dataModels/chatMessageResponse.ts";
import useAsyncEffect from "use-async-effect";

interface Props {
    document: DocumentResponse;
    sentences: string[];
}

export const DocumentDetail = (props: Props) => {
    const [currentLine, setCurrentLine] = useState(props.document.currentLine || 0);
    const [correctedText, setCorrectedText] = useState('');
    const [translatedText, setTranslatedText] = useState('');
    const [isCorrected, setIsCorrected] = useState(false);
    const [isDisplayCorrectionOutput, setIsDisplayCorrectionOutput] = useState(false);
    const [chatMessageResponse, setChatMessageResponse] = useState<ChatMessageResponse[]>([]);
    const { t } = useTranslation();

    useAsyncEffect(async () => {
        const response = await  getConversationThread(props.document.threadId);
        setChatMessageResponse(response);
    }, [props.document.threadId]);

    const methods = useForm<TDocumentDetailsSchema>({
        resolver: zodResolver(DocumentDetailsSchema),
    });

    const { register, handleSubmit, reset, formState: { errors, isSubmitting } } = methods;

    const isDocumentFinished = useMemo(() => {
        return currentLine >= props.sentences.length;
    }, [currentLine, props.sentences.length]);

    const onSubmit = async (data: { translatedText: string }) => {
        const { translatedText } = data;

        const { isCorrected, correctedText } = await correctText({
            originalText: props.sentences[currentLine],
            translatedText: translatedText,
            threadId: props.document.threadId,
        })

        setTranslatedText(translatedText);
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
                    {isDocumentFinished ? (
                            <div className="flex justify-end mt-2">
                                <Button onClick={handleStartAgain}>{t('startAgain')}</Button>
                            </div>
                        ) :
                        (
                            <FormProvider {...methods}>
                                <form onSubmit={handleSubmit(onSubmit)} className="flex flex-col mb-4">
                                    <Textarea
                                        {...register('translatedText', { required: true })}
                                        placeholder={t('enterYourText')}
                                        disabled={isSubmitting} />
                                    <FormMessage>{errors.translatedText?.message}</FormMessage>
                                    <div className="flex justify-end mt-2">
                                        <Button type="submit" disabled={isSubmitting}>{t('send')}</Button>
                                    </div>
                                    {isSubmitting && <div
                                        className="text-center mt-4">{t('loading')}...</div>}
                                </form>
                            </FormProvider>
                        )}
                    {isDisplayCorrectionOutput ?
                        <DocumentCorrectionOutput
                            originalText={translatedText}
                            correctedText={correctedText}
                            isCorrected={isCorrected} />
                        : null
                    }
                </div>
                <ChatBotToggle chatMessageResponse={chatMessageResponse} threadId={props.document.threadId} />
            </div>
        </div>
    )
}