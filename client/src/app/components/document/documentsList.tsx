import {DocumentListItemComponent} from "@/app/components/document/documentListItemComponent.tsx";
import {DocumentListItem} from "@/app/dataModels/document/documentListItem.ts";
import {useTranslation} from "react-i18next";

interface Props {
    allDocuments: DocumentListItem[];
    onDelete(documentId: string): void;
}

export const DocumentsList = (props: Props) => {
    const { t } = useTranslation();

    return (
        <main>
            <div className="flex justify-center items-center">
                <div className="grid gap-8 p-4 grid-cols-1 grid-md:grid-cols-2 grid-lg:grid-cols-3">
                    {props.allDocuments && props.allDocuments.length > 0 ?
                        props.allDocuments.map(document =>
                            <DocumentListItemComponent
                                key={document.id}
                                document={document}
                                onDelete={props.onDelete}
                            />)
                        : <p className='text-gray-400'>{t('noDocumentsAvailable')}</p>
                    }
                </div>
            </div>
        </main>
    );
};