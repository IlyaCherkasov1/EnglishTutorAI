import {useState} from "react";
import {getAllDocuments} from "@/app/api/document/documentApi.ts";
import {DocumentListItem} from "@/app/dataModels/document/documentListItem.ts";
import {DocumentsList} from "@/app/components/component/document/documentsList.tsx";
import useAsyncEffect from "use-async-effect";

const Home = () => {
    const [documents, setDocuments] = useState<DocumentListItem[]>([]);

    useAsyncEffect(async () => {
        const documentsData = await getAllDocuments();
        setDocuments(documentsData);
    }, []);

    if (documents.length === 0) {
        return null;
    }

    return <DocumentsList allDocuments={documents} />;
};

export default Home;