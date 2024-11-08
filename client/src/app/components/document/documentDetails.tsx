import {getDocumentDetails} from "@/app/api/document/documentApi.ts";
import {DocumentDetail} from "@/app/components/document/documentDetail.tsx";
import {useState} from "react";
import {DocumentResponse} from "@/app/dataModels/document/documentResponse.ts";
import {useParams} from "react-router-dom";
import useAsyncEffect from "use-async-effect";

const DocumentDetails = () => {
    const { documentId } = useParams<{ documentId: string }>();
    const [document, setDocument] = useState<DocumentResponse>();

    useAsyncEffect(async () => {
        const documentData = await getDocumentDetails(documentId!);
        setDocument(documentData);
    }, [documentId]);

    if (!document || document.sentences.length === 0) {
        return null;
    }

    return (
        <div className="flex flex-col h-screen">
            <DocumentDetail document={document} />
        </div>
    )
};

export default DocumentDetails;