import {DocumentListItemComponent} from "@/app/components/document/documentListItemComponent.tsx";
import {DocumentListItem} from "@/app/dataModels/document/documentListItem.ts";

interface Props {
    allDocuments: DocumentListItem[];
    onDelete(documentId: string): void;
}

export const DocumentsList = (props: Props) => {
    if (!props.allDocuments || props.allDocuments.length === 0) {
        return null;
    }

    return (
        <main>
            <div className="flex justify-center items-center">
                <div className="grid gap-8 p-4 grid-cols-1 grid-md:grid-cols-2 grid-lg:grid-cols-3">
                    {props.allDocuments.map((document, index) =>
                        <DocumentListItemComponent
                            key={document.id}
                            document={document}
                            onDelete={props.onDelete}
                            index={index}
                        />)}
                </div>
            </div>
        </main>
    );
};