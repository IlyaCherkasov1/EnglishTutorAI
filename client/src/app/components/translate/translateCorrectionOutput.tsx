import {useTranslation} from "react-i18next";
import DiffComponent from "@/app/components/chatBot/diffComponent.tsx";
import {ExplanationCard} from "@/app/components/chatBot/explanationCard.tsx";

interface Props {
    translatedText: string;
    correctedText: string;
    isCorrected: boolean;
    threadId: string;
    currentLine: number;
    userTranslateId: string;
}

const TranslateCorrectionOutput = (props: Props) => {
    const { t } = useTranslation();

    return (
        <div>
            <div className="bg-gray-100 p-4 rounded-md">
                {props.isCorrected ? (
                    <>
                        <h3 className="font-bold mb-2">{t('correctedText')}:</h3>
                        <DiffComponent translatedText={props.translatedText} correctedText={props.correctedText} />
                    </>
                ) : (
                    <h3 className="font-bold mb-2">{t('translationSuccessMessage')}</h3>
                )}
            </div>
            {props.isCorrected && (
                <div className="mt-4">
                    <ExplanationCard
                        threadId={props.threadId}
                        currentLine={props.currentLine}
                        isCorrected={props.isCorrected}
                        userTranslateId={props.userTranslateId}
                    />
                </div>
            )}
        </div>
    );
};

export default TranslateCorrectionOutput;