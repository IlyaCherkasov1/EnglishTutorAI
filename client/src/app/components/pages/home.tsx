import {useState} from "react";
import {getDocuments} from "@/app/api/document/documentApi.ts";
import {DocumentsList} from "@/app/components/document/documentsList.tsx";
import useAsyncEffect from "use-async-effect";
import {Constants} from "@/app/infrastructure/common/constants.ts";
import {Pageable} from "@/app/dataModels/common/pageable.ts";
import {DocumentFilteredData} from "@/app/dataModels/document/documentFilteredData.ts";
import {getNameOfFunction} from "@/app/infrastructure/utils/namingUtils.ts";
import {DocumentSearchResult} from "@/app/dataModels/document/documentSearchResult.ts";
import {queryStringToObject} from "@/app/infrastructure/utils/paramsUtils.ts";
import {SearchPanel} from "@/app/components/document/searchPanel.tsx";
import {useLocation} from "react-router-dom";
import {PageSwitcher} from "@/app/components/pagination/pageSwitcher.tsx";

const Home = () => {
    const [documentSearchResult, setDocumentsSearchResult] = useState<DocumentSearchResult>();
    const [pageNumber, setPageNumber] = useState(1);
    const [totalPages, setTotalPages] = useState(1);
    const location = useLocation();

    const paging: Pageable = {
        pageNumber,
        pageSize: Constants.defaultPageSize,
    };

    const nameOfDocumentsFilterDataProperty = getNameOfFunction<DocumentFilteredData>();

    const getFilterDataQuery = async (): Promise<DocumentFilteredData> => {
        const params = queryStringToObject(window.location.search);
        const studyTopic = params[nameOfDocumentsFilterDataProperty("studyTopic")];

        return {
            studyTopic: studyTopic,
        } as DocumentFilteredData;
    }

    useAsyncEffect(async () => {
        const filteredData = await getFilterDataQuery();
        const requestData = { ...filteredData, ...paging };
        const documentsData = await getDocuments(requestData);

        setDocumentsSearchResult(documentsData);

        const newTotalPages = Math.max(1, Math.ceil(documentsData.totalCount / paging.pageSize));
        setTotalPages(newTotalPages);

        if (location.search) {
            setPageNumber(1);
        }
    }, [location.search, pageNumber]);

    if (!documentSearchResult) {
        return null;
    }

    return (
        <>
            <div className="max-w-8xl mx-auto px-4">
                <SearchPanel />
            </div>
            <div className="max-w-8xl mx-auto px-4 flex flex-col min-h-[75vh]">
                <DocumentsList allDocuments={documentSearchResult.items} />
                <div className="mt-auto flex justify-center">
                    <PageSwitcher
                        currentPage={pageNumber}
                        totalPages={totalPages}
                        onPageChange={setPageNumber}
                    />
                </div>
            </div>
        </>
    )
};

export default Home;