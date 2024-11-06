import {useEffect, useState} from "react";
import {DocumentListItemComponent} from "@/app/components/document/documentListItemComponent.tsx";
import {DocumentListItem} from "@/app/dataModels/document/documentListItem.ts";
import {useTranslation} from "react-i18next";

interface Props {
    allDocuments: DocumentListItem[];
}

export const DocumentsList = (props: Props) => {
    const [documents, setDocuments] = useState(props.allDocuments);
    const { t } = useTranslation();

    useEffect(() => {
        setDocuments(props.allDocuments);
    }, [props.allDocuments]);

    const handleDeleteDocument = (documentId: string) => {
        setDocuments(documents?.filter(document => document.id !== documentId) || []);
    }

    return (
        <main>
            <div className="flex justify-center items-center">
                <div className="grid grid-cols-3 gap-5 w-full m-5">
                    {documents && documents.length > 0 ?
                        documents.map(document =>
                            <DocumentListItemComponent
                                key={document.id}
                                document={document}
                                onDelete={handleDeleteDocument}
                            />)
                        : <p className='text-gray-400'>{t('noDocumentsAvailable')}</p>
                    }
                </div>
            </div>
        </main>
    );
};