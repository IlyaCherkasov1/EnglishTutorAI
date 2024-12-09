import {useCallback, useState} from "react";
import {deleteDocument, getDocuments, getNextDocument} from "@/app/api/documentApi.ts";
import {DocumentsList} from "@/app/components/document/documentsList.tsx";
import {Constants} from "@/app/infrastructure/constants/constants.ts";
import {Pageable} from "@/app/dataModels/common/pageable.ts";
import {DocumentListItem} from "@/app/dataModels/document/documentListItem.ts";
import {ContentLoaderSpinner} from "@/app/components/ui/contentLoaderSpinner.tsx";
import {InfiniteScroll} from "@/app/components/pagination/InfiniteScroll.tsx";
import {StudyTopic} from "@/app/dataModels/enums/studyTopic.ts";
import {CategorySelector} from "@/app/components/categorySelector.tsx";

const Home = () => {
    const [documentListItem, setDocumentListItem] = useState<DocumentListItem[]>([]);
    const [page, setPage] = useState(1);
    const [totalCount, setTotalCount] = useState<number | null>(null);
    const [hasMore, setHasMore] = useState(true);
    const [selectedCategory, setSelectedCategory] = useState<string>(StudyTopic[StudyTopic.All]);
    const [isInitialLoad, setIsInitialLoad] = useState(true);

    const paging: Pageable = {
        pageNumber: page,
        pageSize: Constants.documentsPageSize,
    };

    const loadMoreItems = async () => {
        const response = await getDocuments({
            studyTopic: selectedCategory,
            ...paging
        });

        if (isInitialLoad) {
            setIsInitialLoad(false);
        }

        if (totalCount === null) {
            setTotalCount(response.totalCount);
        }

        setDocumentListItem((prev) => [...prev, ...response.items]);

        const totalLoaded = documentListItem.length + response.items.length;
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

        const response = await getDocuments({
            studyTopic: category,
            pageSize: Constants.documentsPageSize,
            pageNumber: 1,
        });

        setTotalCount(response.totalCount);
        setDocumentListItem(response.items);

        const hasMore =response.items.length < (response.totalCount || 0);
        setHasMore(hasMore);

        if (hasMore) {
            setPage((prev) => prev + 1);
        }
    };

    const fetchNewDocument = useCallback(async () => {
        const lastDocumentCreatedAt = documentListItem[documentListItem.length - 1].createdAt;

        const document = await getNextDocument({
            studyTopic: selectedCategory,
            createdAt: lastDocumentCreatedAt
        });

        if (document) {
            setDocumentListItem((prev) => [...prev, document]);
        }
    }, [setDocumentListItem, selectedCategory, documentListItem])


    const handleDeleteDocument = useCallback(async (documentId: string) => {
        await deleteDocument(documentId);
        setDocumentListItem((prevDocuments) =>
            prevDocuments.filter((document) => document.id !== documentId)
        );

        setTotalCount((prevState) => (prevState !== null ? prevState - 1 : prevState));

        if (documentListItem.length !== totalCount) {
            await fetchNewDocument();
        }

    }, [fetchNewDocument, setTotalCount, documentListItem, totalCount]);

    return (
        <div className="pb-5">
            <CategorySelector selectedCategory={selectedCategory} onCategoryChange={handleCategoryChange} />
            <div className="mt-8">
                <InfiniteScroll
                    loadMore={loadMoreItems}
                    hasMore={hasMore}
                    loader={<ContentLoaderSpinner />}
                    isInitialLoad={isInitialLoad}>
                    <DocumentsList allDocuments={documentListItem} onDelete={handleDeleteDocument} />
                </InfiniteScroll>
            </div>
        </div>
    )
};

export default Home;