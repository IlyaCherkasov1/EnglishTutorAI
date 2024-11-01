import {Card, CardContent, CardFooter, CardHeader} from "../ui/card"
import {getMistakeHistoryItems} from "@/app/api/document/documentApi.ts";
import useAsyncEffect from "use-async-effect";
import {useState} from "react";
import DiffComponent from "@/app/components/chatBot/diffComponent.tsx";
import {formatDateToISO} from "@/app/infrastructure/helpers/dateHelpers.ts";
import InfiniteScroll from "react-infinite-scroll-component";
import {MistakeHistoryItems} from "@/app/dataModels/mistakeHistoryItems.ts";
import {useTranslation} from "react-i18next";
import {LoadingSpinner} from "@/app/components/ui/loadingSpinner.tsx";

export const MistakeHistory = () => {
    const [mistakeHistoryItems, setMistakeHistoryItems] = useState<Array<MistakeHistoryItems>>([]);
    const [page, setPage] = useState(1);
    const [hasMore, setHasMore] = useState(true);
    const { t } = useTranslation();

    const loadMoreItems = async () => {
        const historyItems = await getMistakeHistoryItems({ pageNumber: page, pageSize: 10 });

        if (historyItems.length === 0) {
            setHasMore(false);
        } else {
            setMistakeHistoryItems(prev => [...prev, ...historyItems]);
            setPage(prev => prev + 1);
        }
    };

    useAsyncEffect(async () => {
        await loadMoreItems();
    }, []);

    if (mistakeHistoryItems.length === 0) {
        return (
            <div className="flex flex-col items-center">
                <h1 className="text-4xl text-gray-900 mb-3">{t('history')}</h1>
                <p className="text-lg text-gray-600">{t('noRecords')}</p>
            </div>
        );
    }

    return (
        <>
            <div className="flex flex-col items-center">
                <h1 className="text-4xl text-gray-900 mb-3">{t('history')}</h1>
                <InfiniteScroll
                    dataLength={mistakeHistoryItems.length}
                    next={loadMoreItems}
                    hasMore={hasMore}
                    loader={<LoadingSpinner />}
                    className="flex flex-col items-center gap-4r">
                    {mistakeHistoryItems?.map(item => (
                        <Card className="w-4/5 mb-4" key={item.id}>
                            <CardHeader>
                                {item.translatedText}
                            </CardHeader>
                            <CardContent>
                                <DiffComponent
                                    translatedText={item.translatedText}
                                    correctedText={item.correctedText} />
                            </CardContent>
                            <CardFooter>
                                <p className="text-sm text-gray-500">{formatDateToISO(item.createdAt)}</p>
                            </CardFooter>
                        </Card>
                    ))}
                </InfiniteScroll>
            </div>
        </>
    )
}