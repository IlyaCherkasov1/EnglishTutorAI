import {useState} from "react";
import {MistakeHistoryItems} from "@/app/dataModels/mistakeHistoryItems.ts";
import {useTranslation} from "react-i18next";
import {ContentLoaderSpinner} from "@/app/components/ui/contentLoaderSpinner.tsx";
import {Constants} from "@/app/infrastructure/constants/constants.ts";
import {InfiniteScroll} from "@/app/components/pagination/InfiniteScroll.tsx";
import {getMistakeHistoryItems} from "@/app/api/documentHisoryApi.ts";
import {MistakeCard} from "@/app/components/mistakeCard.tsx";

export const MistakeHistory = () => {
    const [mistakeHistoryItems, setMistakeHistoryItems] = useState<MistakeHistoryItems[]>([]);
    const [page, setPage] = useState(1);
    const [totalCount, setTotalCount] = useState<number | null>(null);
    const [hasMore, setHasMore] = useState(true);
    const [isInitialLoad, setIsInitialLoad] = useState(true);
    const { t } = useTranslation();

    const loadMoreItems = async () => {
        const response = await getMistakeHistoryItems(
            { pageNumber: page, pageSize: Constants.mistakeHistoryPageSize });

        if (isInitialLoad) {
            setIsInitialLoad(false);
        }

        if (totalCount === null) {
            setTotalCount(response.totalCount);
        }

        setMistakeHistoryItems(prev => [...prev, ...response.items]);
        const totalLoaded = mistakeHistoryItems.length + response.items.length;
        setHasMore(totalLoaded < (response.totalCount || 0));

        if (totalLoaded < (response.totalCount || 0)) {
            setPage((prev) => prev + 1);
        }
    };

    return (
        <div className="flex flex-col pb-5">
            <h1 className="text-4xl font-semibold">{t('history')}</h1>
            <InfiniteScroll
                loadMore={loadMoreItems}
                hasMore={hasMore}
                loader={<ContentLoaderSpinner />}
                isInitialLoad={isInitialLoad}>
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
    )
}