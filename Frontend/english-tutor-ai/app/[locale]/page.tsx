import {getAllDocuments} from "@/app/api/document/documentApi";
import {DocumentsList} from "@/app/components/component/document/documentsList";
import {auth} from "@/auth";

export default async function Home() {
    const session = await auth()
    if (!session?.user) return null

    const allDocuments = await getAllDocuments();

    return <DocumentsList allDocuments={allDocuments}/>
}