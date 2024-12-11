import {useState} from "react";
import useAsyncEffect from "use-async-effect";
import {getCurrentTranslateSessionMistakesHistory} from "@/app/api/translateHistoryApi.ts";
import {TranslateMistakeHistoryItems} from "@/app/dataModels/translate/translateMistakeHistoryItems.ts";
import {useTranslation} from "react-i18next";
import {MistakeCard} from "@/app/components/mistakeCard.tsx";

interface Props {
    userTranslateId: string;
}

export const TranslateResult = (props: Props) => {
    const [mistakeHistoryItems, setMistakeHistoryItems] = useState<Array<TranslateMistakeHistoryItems>>([]);
    const { t } = useTranslation();

    useAsyncEffect(async () => {
        const mistakes = await getCurrentTranslateSessionMistakesHistory(props.userTranslateId);
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