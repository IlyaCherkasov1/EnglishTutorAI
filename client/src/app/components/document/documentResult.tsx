import {useState} from "react";
import useAsyncEffect from "use-async-effect";
import {getDocumentSessionMistakesHistory} from "@/app/api/documentHisoryApi.ts";
import {DocumentMistakeHistoryItems} from "@/app/dataModels/document/documentMistakeHistoryItems.ts";
import {Card, CardContent, CardHeader} from "@/app/components/ui/card.tsx";
import DiffComponent from "@/app/components/chatBot/diffComponent.tsx";
import {useTranslation} from "react-i18next";

interface Props {
    sessionId: string;
}

export const DocumentResult = (props: Props) => {
    const [mistakeHistoryItems, setMistakeHistoryItems] = useState<Array<DocumentMistakeHistoryItems>>([]);
    const { t } = useTranslation();

    useAsyncEffect(async () => {
        const mistakes = await getDocumentSessionMistakesHistory(props.sessionId);
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
                    <Card className="mb-4" key={item.id}>
                        <CardHeader>
                            {item.translatedText}
                        </CardHeader>
                        <CardContent>
                            <DiffComponent
                                translatedText={item.translatedText}
                                correctedText={item.correctedText} />
                        </CardContent>
                    </Card>
                ))}
            </div>
        </div>
    )
}