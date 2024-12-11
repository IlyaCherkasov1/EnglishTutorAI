import {TranslateDetailsModel} from "@/app/dataModels/translate/translateDetailsModel.ts";
import React, {useState} from "react";
import {useTranslation} from "react-i18next";
import {
    getConversationThread,
    handleTranslateCompletion,
    handleTranslateStart,
    saveCurrentLine
} from "@/app/api/translateApi.ts";
import {Button} from "@/app/components/ui/button.tsx";
import {Textarea} from "@/app/components/ui/textarea.tsx";
import TranslateCorrectionOutput from "@/app/components/translate/translateCorrectionOutput.tsx";
import {FormProvider, useForm} from "react-hook-form";
import {correctText} from "@/app/api/languageModelApi.ts";
import ChatBotToggle from "@/app/components/chatBot/chatBotToggle.tsx";
import {zodResolver} from "@hookform/resolvers/zod";
import {FormMessage} from "@/app/components/ui/form.tsx";
import {
    TranslateDetailsSchema,
    TypeTranslateDetailsSchema
} from "@/app/infrastructure/validationSchemas/translateDetailsSchema.ts";
import {ChatMessageResponse} from "@/app/dataModels/chatMessageResponse.ts";
import useAsyncEffect from "use-async-effect";
import {TranslateResult} from "@/app/components/translate/translateResult.tsx";
import {SubmitSpinner} from "@/app/components/ui/submitSpinner.svg.tsx";

interface Props {
    translateDetails: TranslateDetailsModel;
}

export const TranslateDetail = (props: Props) => {
    const [currentLine, setCurrentLine] = useState(props.translateDetails.currentLine);
    const [correctedText, setCorrectedText] = useState('');
    const [translatedText, setTranslatedText] = useState('');
    const [chatMessageResponse, setChatMessageResponse] = useState<ChatMessageResponse[]>([]);
    const [isCorrected, setIsCorrected] = useState(false);
    const [isTranslateFinished, setIsTranslateFinished] = useState(props.translateDetails.isTranslateFinished);

    const { t } = useTranslation();

    useAsyncEffect(async () => {
        const response = await getConversationThread(props.translateDetails.threadId);
        setChatMessageResponse(response);
    }, [props.translateDetails.threadId]);

    const methods = useForm<TypeTranslateDetailsSchema>({ resolver: zodResolver(TranslateDetailsSchema) });
    const { register, handleSubmit, reset, setFocus, formState: { errors, isSubmitting } } = methods;

    const onSubmit = async (data: { translatedText: string }) => {
        const { translatedText } = data;

        const result = await correctText({
            originalText: props.translateDetails.sentences[currentLine],
            translatedText: translatedText,
            threadId: props.translateDetails.threadId,
            userTranslateId: props.translateDetails.userTranslateId,
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

        if (nextLine >= props.translateDetails.sentences.length) {
            setIsTranslateFinished(true);
            await handleTranslateCompletion(props.translateDetails.userTranslateId);
        }

        await saveCurrentLine({ currentLine: currentLine + 1, userTranslateId: props.translateDetails.userTranslateId });

        reset();
        setFocus("translatedText");
    };

    const handleStartAgain = async () => {
        await handleTranslateStart(props.translateDetails.userTranslateId);

        setIsTranslateFinished(false)
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
                <h1 className="text-3xl font-bold mb-6">{props.translateDetails.title}</h1>
                <div className="bg-gray-100 p-4 rounded-md mb-4">
                    {props.translateDetails.sentences.map((line, index) => (
                        <span
                            key={index}
                            className={`mr-1 ${index === currentLine ? 'text-black' : 'text-gray-400'}`}>
                                {line}
                            </span>
                    ))}
                </div>
                {isTranslateFinished ? (
                    <>
                        <div className="flex justify-end mt-2">
                            <Button onClick={handleStartAgain}>{t('startAgain')}</Button>
                        </div>
                        <TranslateResult userTranslateId={props.translateDetails.userTranslateId} />
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
                            <TranslateCorrectionOutput
                                translatedText={translatedText}
                                correctedText={correctedText}
                                isCorrected={isCorrected}
                                threadId={props.translateDetails.threadId}
                                currentLine={currentLine}
                                userTranslateId={props.translateDetails.userTranslateId}
                            />
                        }
                    </>
                )}
                <ChatBotToggle
                    chatMessageResponse={chatMessageResponse}
                    threadId={props.translateDetails.threadId}
                    userTranslateId={props.translateDetails.userTranslateId} />
            </div>
        </div>
    )
}