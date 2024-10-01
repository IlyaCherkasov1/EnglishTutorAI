import {useState} from "react";
import {DocumentListItem} from "@/app/dataModels/document/documentListItem.ts";
import {DocumentListItemComponent} from "@/app/components/component/document/documentListItemComponent.tsx";

interface Props {
    allDocuments: DocumentListItem[];
}

export const DocumentsList = (props: Props) => {
    const [documents, setDocuments] = useState(props.allDocuments);

    const handleDeleteDocument = (documentId: string) => {
        setDocuments(documents.filter(document => document.id !== documentId));
    }

    if (documents.length === 0) {
        return null;
    }

    return (
        <main>
            <div className="bg-gray-100">
                <ul className="divide-y divide-gray-200">
                    {documents.map(document => (
                        <DocumentListItemComponent
                            key={document.id}
                            document={document}
                            onDelete={handleDeleteDocument}
                        />
                    ))}
                </ul>
            </div>
        </main>
    );
};