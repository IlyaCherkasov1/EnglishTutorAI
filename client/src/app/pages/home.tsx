import {useEffect, useState} from "react";
import {getAllDocuments} from "@/app/api/document/documentApi.ts";
import {DocumentListItem} from "@/app/dataModels/document/documentListItem.ts";
import {DocumentsList} from "@/app/components/component/document/documentsList.tsx";

const Home = () => {
    const [documents, setDocuments] = useState<DocumentListItem[]>([]);

    useEffect(() => {
        const fetchAllDocuments = async () => {
            const documentsData = await getAllDocuments();
            setDocuments(documentsData);
        }

        fetchAllDocuments().catch(console.error);
    }, []);

    return (
        <>
            {documents.length > 0 ? (
                <DocumentsList allDocuments={documents}/>
            ) : (
                <div>Loading documents...</div>
            )}
        </>
    );
};

export default Home;