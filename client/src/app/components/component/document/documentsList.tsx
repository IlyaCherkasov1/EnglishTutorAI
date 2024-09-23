import {useTranslation} from "react-i18next";
import {useState} from "react";
import {DocumentListItem} from "../../../dataModels/document/documentListItem.ts";
import {DocumentListItemComponent} from "./documentListItemComponent.tsx";

interface Props {
    allDocuments: DocumentListItem[];
}

export const DocumentsList = (props: Props) => {
    const [documents, setDocuments] = useState(props.allDocuments);
    const { t } = useTranslation();

    const handleDeleteDocument = (documentId: string) => {
        setDocuments(documents.filter(document => document.id !== documentId));
    }

    return (
        <main>
            <div className="bg-gray-100">
                <ul className="divide-y divide-gray-200">
                    {documents ? (
                        documents.map(document => (
                            <DocumentListItemComponent
                                key={document.id}
                                document={document}
                                onDelete={handleDeleteDocument} />
                        ))
                    ) : (
                        <li className="py-4">
                            <span className="text-gray-700">{t('loading')}</span>
                        </li>
                    )}
                </ul>
            </div>
        </main>
    );
};