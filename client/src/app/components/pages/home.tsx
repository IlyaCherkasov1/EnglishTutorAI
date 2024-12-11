import {useCallback, useState} from "react";
import {deleteTranslate, getTranslates, getNextTranslate} from "@/app/api/translateApi.ts";
import {TranslatesList} from "@/app/components/translate/translatesList.tsx";
import {Constants} from "@/app/infrastructure/constants/constants.ts";
import {Pageable} from "@/app/dataModels/common/pageable.ts";
import {TranslateListItem} from "@/app/dataModels/translate/translateListItem.ts";
import {ContentLoaderSpinner} from "@/app/components/ui/contentLoaderSpinner.tsx";
import {InfiniteScroll} from "@/app/components/pagination/InfiniteScroll.tsx";
import {StudyTopic} from "@/app/dataModels/enums/studyTopic.ts";
import {CategorySelector} from "@/app/components/categorySelector.tsx";

const Home = () => {
    const [translateListItem, setTranslateListItem] = useState<TranslateListItem[]>([]);
    const [page, setPage] = useState(1);
    const [totalCount, setTotalCount] = useState<number | null>(null);
    const [hasMore, setHasMore] = useState(true);
    const [selectedCategory, setSelectedCategory] = useState<string>(StudyTopic[StudyTopic.All]);
    const [isInitialLoad, setIsInitialLoad] = useState(true);

    const paging: Pageable = {
        pageNumber: page,
        pageSize: Constants.translatesPageSize,
    };

    const loadMoreItems = async () => {
        const response = await getTranslates({
            studyTopic: selectedCategory,
            ...paging
        });

        if (isInitialLoad) {
            setIsInitialLoad(false);
        }

        if (totalCount === null) {
            setTotalCount(response.totalCount);
        }

        setTranslateListItem((prev) => [...prev, ...response.items]);

        const totalLoaded = translateListItem.length + response.items.length;
        setHasMore(totalLoaded < (response.totalCount || 0));

        if (totalLoaded < (response.totalCount || 0)) {
            setPage((prev) => prev + 1);
        }
    };

    const handleCategoryChange = async (category: string) => {
        setSelectedCategory(category);
        setPage(1);
        setTotalCount(null);
        setIsInitialLoad(true);

        const response = await getTranslates({
            studyTopic: category,
            pageSize: Constants.translatesPageSize,
            pageNumber: 1,
        });

        setTotalCount(response.totalCount);
        setTranslateListItem(response.items);

        const hasMore =response.items.length < (response.totalCount || 0);
        setHasMore(hasMore);

        if (hasMore) {
            setPage((prev) => prev + 1);
        }
    };

    const fetchNewTranslate = useCallback(async () => {
        const lastTranslateCreatedAt = translateListItem[translateListItem.length - 1].createdAt;

        const translate = await getNextTranslate({
            studyTopic: selectedCategory,
            createdAt: lastTranslateCreatedAt
        });

        if (translate) {
            setTranslateListItem((prev) => [...prev, translate]);
        }
    }, [setTranslateListItem, selectedCategory, translateListItem])


    const handleDeleteTranslate = useCallback(async (translateId: string) => {
        await deleteTranslate(translateId);
        setTranslateListItem((prevTranslates) =>
            prevTranslates.filter((translate) => translate.id !== translateId)
        );

        setTotalCount((prevState) => (prevState !== null ? prevState - 1 : prevState));

        if (translateListItem.length !== totalCount) {
            await fetchNewTranslate();
        }

    }, [fetchNewTranslate, setTotalCount, translateListItem, totalCount]);

    return (
        <div className="pb-5">
            <CategorySelector selectedCategory={selectedCategory} onCategoryChange={handleCategoryChange} />
            <div className="mt-8">
                <InfiniteScroll
                    loadMore={loadMoreItems}
                    hasMore={hasMore}
                    loader={<ContentLoaderSpinner />}
                    isInitialLoad={isInitialLoad}>
                    <TranslatesList allTranslates={translateListItem} onDelete={handleDeleteTranslate} />
                </InfiniteScroll>
            </div>
        </div>
    )
};

export default Home;