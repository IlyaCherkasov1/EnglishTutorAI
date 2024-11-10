import {useState} from "react";
import {getDocuments} from "@/app/api/document/documentApi.ts";
import {DocumentsList} from "@/app/components/document/documentsList.tsx";
import {Constants} from "@/app/infrastructure/constants/constants.ts";
import {Pageable} from "@/app/dataModels/common/pageable.ts";
import {SearchPanel} from "@/app/components/document/searchPanel.tsx";
import {DocumentListItem} from "@/app/dataModels/document/documentListItem.ts";
import {LoadingSpinner} from "@/app/components/ui/loadingSpinner.tsx";
import {InfiniteScroll} from "@/app/components/pagination/InfiniteScroll.tsx";
import {StudyTopic} from "@/app/dataModels/enums/studyTopic.ts";

const Home = () => {
    const [documentListItem, setDocumentListItem] = useState<Array<DocumentListItem>>([]);
    const [page, setPage] = useState(1);
    const [hasMore, setHasMore] = useState(true);
    const [selectedCategory, setSelectedCategory] = useState<string>(StudyTopic[StudyTopic.All]);

    const paging: Pageable = {
        pageNumber: page,
        pageSize: Constants.documentsPageSize,
    };

    const loadMoreItems = async () => {
        const documents = await getDocuments({
            studyTopic: selectedCategory,
            ...paging
        });

        if (documents.length === 0) {
            setHasMore(false);
        } else {
            setDocumentListItem(prev => [...prev, ...documents]);
            setPage(prev => prev + 1);
        }
    };


    const handleCategoryChange = async (category: string) => {
        setSelectedCategory(category);
        setPage(1);

        const documents = await getDocuments({
            studyTopic: category,
            ...paging
        });

        if (documents.length < Constants.documentsPageSize) {
            setHasMore(false);
        } else {
            setHasMore(true);
        }

        setDocumentListItem(documents);
    };

    return (
        <div>
            <div className="max-w-8xl mx-auto px-4">
                <SearchPanel selectedCategory={selectedCategory} onCategoryChange={handleCategoryChange} />
            </div>
            <div className="max-w-8xl mx-auto px-4 flex flex-col min-h-[75vh]">
                <InfiniteScroll
                    loadMore={loadMoreItems}
                    hasMore={hasMore}
                    loader={<LoadingSpinner />}>
                    <DocumentsList allDocuments={documentListItem} />
                </InfiniteScroll>
            </div>
        </div>
    )
};

export default Home;