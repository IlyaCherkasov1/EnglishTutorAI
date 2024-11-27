import {useState} from "react";
import {MistakeHistoryItems} from "@/app/dataModels/mistakeHistoryItems.ts";
import {useTranslation} from "react-i18next";
import {LoadingSpinner} from "@/app/components/ui/loadingSpinner.tsx";
import {Constants} from "@/app/infrastructure/constants/constants.ts";
import {InfiniteScroll} from "@/app/components/pagination/InfiniteScroll.tsx";
import {getMistakeHistoryItems} from "@/app/api/documentHisoryApi.ts";
import {MistakeCard} from "@/app/components/mistakeCard.tsx";

export const MistakeHistory = () => {
    const [mistakeHistoryItems, setMistakeHistoryItems] = useState<Array<MistakeHistoryItems>>([]);
    const [page, setPage] = useState(1);
    const [hasMore, setHasMore] = useState(true);
    const { t } = useTranslation();

    const loadMoreItems = async () => {
        const historyItems = await getMistakeHistoryItems(
            { pageNumber: page, pageSize: Constants.mistakeHistoryPageSize });

        if (historyItems.length === 0) {
            setHasMore(false);
        } else {
            setMistakeHistoryItems(prev => [...prev, ...historyItems]);
            setPage(prev => prev + 1);
        }
    };

    return (
        <div className="flex flex-col">
            <div className="mt-10">
                <h1 className="text-4xl font-semibold">{t('history')}</h1>
                <InfiniteScroll
                    loadMore={loadMoreItems}
                    hasMore={hasMore}
                    loader={<LoadingSpinner />}>
                    <div className="flex flex-col items-center mt-7">
                        {mistakeHistoryItems.length === 0 && !hasMore && (
                            <p className="text-gray-500 mt-4">{t('noRecords')}</p>
                        )}
                        {mistakeHistoryItems?.map(item => (
                           <MistakeCard
                               id={item.id}
                               translatedText={item.translatedText}
                               correctedText={item.correctedText}
                               createdAt={item.createdAt}
                               documentId={item.documentId}>
                           </MistakeCard>
                        ))}
                    </div>
                </InfiniteScroll>
            </div>
        </div>
    )
}