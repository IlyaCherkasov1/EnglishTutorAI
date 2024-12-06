import {useState} from "react";
import useAsyncEffect from "use-async-effect";
import {getCurrentDocumentSessionMistakesHistory} from "@/app/api/documentHisoryApi.ts";
import {DocumentMistakeHistoryItems} from "@/app/dataModels/document/documentMistakeHistoryItems.ts";
import {useTranslation} from "react-i18next";
import {MistakeCard} from "@/app/components/mistakeCard.tsx";

interface Props {
    userDocumentId: string;
}

export const DocumentResult = (props: Props) => {
    const [mistakeHistoryItems, setMistakeHistoryItems] = useState<Array<DocumentMistakeHistoryItems>>([]);
    const { t } = useTranslation();

    useAsyncEffect(async () => {
        const mistakes = await getCurrentDocumentSessionMistakesHistory(props.userDocumentId);
        setMistakeHistoryItems(mistakes);
    }, []);

    return (
        <div>
            <h1 className="text-2xl text-gray-900 mb-3">{t('results')}</h1>
            <div className="flex flex-col">
                {mistakeHistoryItems.length === 0 && (
                    <p className="text-gray-500 mt-4">{t('translationSuccessMessage')}</p>
                )}
                {mistakeHistoryItems?.map(item => (
                    <MistakeCard
                        id={item.id}
                        translatedText={item.translatedText}
                        correctedText={item.correctedText}>
                    </MistakeCard>
                ))}
            </div>
        </div>
    )
}