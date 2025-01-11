import {useTranslation} from "react-i18next";
import {Constants} from "@/app/infrastructure/constants/constants.ts";
import {InfiniteScroll} from "@/app/components/pagination/InfiniteScroll.tsx";
import {ContentLoaderSpinner} from "@/app/components/ui/contentLoaderSpinner.tsx";
import {getCompletedTranslates} from "@/app/api/translateApi.ts";
import {Card, CardContent, CardFooter, CardHeader} from "@/app/components/ui/card.tsx";
import {ArrowRight} from "lucide-react";
import {Link} from "react-router-dom";
import {formatDateToISO} from "@/app/infrastructure/helpers/dateHelpers.ts";
import {useInfiniteScroll} from "@/hooks/useInfiniteScroll.ts";

export const CompletedTranslates = () => {
    const { t } = useTranslation();

    const { items: completedTranslatesItems, hasMore, isInitialLoad, loadMoreItems, totalCount } = useInfiniteScroll(
        (page) => getCompletedTranslates({
            pageNumber: page,
            pageSize: Constants.completedTranslatesPageSize,
        }),
        Constants.completedTranslatesPageSize
    );

    return (
        <div className="flex flex-col pb-5">
            <h1 className="text-3xl font-medium">{t('completedTranslates')}
                {completedTranslatesItems.length !== 0 && (
                    <span className="ml-2 text-gray-600">({totalCount})</span>
                )}
            </h1>
            <InfiniteScroll
                loadMore={loadMoreItems}
                hasMore={hasMore}
                loader={<ContentLoaderSpinner />}
                isInitialLoad={isInitialLoad}>
                <div className="items-center mt-7">
                    {completedTranslatesItems.length === 0 && !hasMore && (
                        <p className="text-gray-500">{t('completedTranslationsNoRecords')}</p>
                    )}
                    {completedTranslatesItems?.map(item => (
                        <Card className="w-full mb-4 hover:shadow-lg transition-shadow" key={item.translateId}>
                            <CardHeader>
                                <h3 className="text-xl font-medium mb-1">{item.title}</h3>
                            </CardHeader>
                            <CardContent>
                                <div className="p-3 bg-gray-100 rounded-md text-gray-800">
                                    <p className="line-clamp-2 text-ellipsis">
                                        {item.content}
                                    </p>
                                </div>
                            </CardContent>
                            <CardFooter>
                                <p className="text-sm text-gray-500">{formatDateToISO(item.completedOn)}</p>
                            </CardFooter>
                            <Link to={`/translates/${item.translateId}`}
                                  className="absolute bottom-4 right-4 flex items-center gap-2 px-4 py-2">
                                {t("goToTranslate")}
                                <ArrowRight size={16} />
                            </Link>
                        </Card>
                    ))}
                </div>
            </InfiniteScroll>
        </div>
    )
}