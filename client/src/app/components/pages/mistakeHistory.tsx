import {useTranslation} from "react-i18next";
import {ContentLoaderSpinner} from "@/app/components/ui/contentLoaderSpinner.tsx";
import {Constants} from "@/app/infrastructure/constants/constants.ts";
import {InfiniteScroll} from "@/app/components/pagination/InfiniteScroll.tsx";
import {getMistakeHistoryItems} from "@/app/api/translateHistoryApi.ts";
import {MistakeCard} from "@/app/components/mistakeCard.tsx";
import {useInfiniteScroll} from "@/hooks/useInfiniteScroll.ts";

export const MistakeHistory = () => {
    const { t } = useTranslation();

    const { items: mistakeHistoryItems, hasMore, isInitialLoad, loadMoreItems } = useInfiniteScroll(
        (page) => getMistakeHistoryItems({
            pageNumber: page,
            pageSize: Constants.mistakeHistoryPageSize,
        }),
        Constants.mistakeHistoryPageSize
    );

    return (
        <div className="flex flex-col pb-5">
            <h1 className="text-3xl font-medium">{t('history')}</h1>
            <InfiniteScroll
                loadMore={loadMoreItems}
                hasMore={hasMore}
                loader={<ContentLoaderSpinner />}
                isInitialLoad={isInitialLoad}>
                <div className="items-center mt-7">
                    {mistakeHistoryItems.length === 0 && !hasMore && (
                        <p className="text-gray-500">{t('historyNoRecordMessage')}</p>
                    )}
                    {mistakeHistoryItems?.map(item => (
                        <MistakeCard
                            id={item.id}
                            translatedText={item.translatedText}
                            correctedText={item.correctedText}
                            createdAt={item.createdAt}
                            translateId={item.translateId}>
                        </MistakeCard>
                    ))}
                </div>
            </InfiniteScroll>
        </div>
    )
}