import {getAllDocuments} from "@/app/api/document/documentApi";
import {DocumentsList} from "@/app/components/component/document/documentsList";

export default async function Home() {
    const allDocuments = await getAllDocuments();

    return <DocumentsList allDocuments={allDocuments}/>
}