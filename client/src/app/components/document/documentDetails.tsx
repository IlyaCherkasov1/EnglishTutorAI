import {getDocumentDetails} from "@/app/api/documentApi.ts";
import {DocumentDetail} from "@/app/components/document/documentDetail.tsx";
import {useState} from "react";
import {DocumentDetailsModel} from "@/app/dataModels/document/documentDetailsModel.ts";
import {useParams} from "react-router-dom";
import useAsyncEffect from "use-async-effect";
import {ContentLoaderSpinner} from "@/app/components/ui/contentLoaderSpinner.tsx";

const DocumentDetails = () => {
    const { documentId } = useParams<{ documentId: string }>();
    const [document, setDocument] = useState<DocumentDetailsModel>();

    useAsyncEffect(async () => {
        const documentData = await getDocumentDetails(documentId!);

        setDocument(documentData);
    }, [documentId]);

    if (!document || document.sentences.length === 0) {
        return <ContentLoaderSpinner />;
    }

    return (
        <div className="flex flex-col h-screen">
            <DocumentDetail documentDetails={document} />
        </div>
    )
};

export default DocumentDetails;