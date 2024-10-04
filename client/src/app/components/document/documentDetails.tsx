import {getDocumentDetails, splitDocumentContent} from "@/app/api/document/documentApi.ts";
import BackButton from "@/app/components/buttons/backButton.tsx";
import {DocumentDetail} from "@/app/components/document/documentDetail.tsx";
import {useState} from "react";
import {DocumentResponse} from "@/app/dataModels/document/documentResponse.ts";
import {useParams} from "react-router-dom";
import useAsyncEffect from "use-async-effect";

const DocumentDetails = () => {
    const { documentId } = useParams<{ documentId: string }>();
    const [document, setDocument] = useState<DocumentResponse>();
    const [sentences, setSentences] = useState<string[]>([]);

    useAsyncEffect(async () => {
        const documentData = await getDocumentDetails(documentId!);
        const splitSentences = await splitDocumentContent({ text: documentData.content });
        setDocument(documentData);
        setSentences(splitSentences);
    }, [documentId]);

    if (!document || sentences.length === 0) {
        return null;
    }

    return (
        <div className="flex flex-col h-screen">
            <div className="p-4">
                <BackButton />
            </div>
            <DocumentDetail document={document} sentences={sentences} />
        </div>
    )
};

export default DocumentDetails;