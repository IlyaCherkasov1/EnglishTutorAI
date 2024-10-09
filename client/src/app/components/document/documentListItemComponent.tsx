import {deleteDocument} from "@/app/api/document/documentApi.ts";
import {DocumentListItem} from "@/app/dataModels/document/documentListItem.ts";
import {Card, CardContent, CardFooter, CardHeader, CardTitle} from "@/app/components/ui/card.tsx";
import {formatDateToISO} from "@/app/infrastructure/helpers/dateHelpers.ts";
import {DeleteDocumentModal} from "@/app/components/modals/deleteDocumentModal.tsx";
import {Link} from "react-router-dom";

interface Props {
    document: DocumentListItem;
    onDelete: (documentId: string) => void;
}

export const DocumentListItemComponent = ({ document, onDelete }: Props) => {
    const handleDelete = async (documentId: string) => {
        await deleteDocument(documentId);
        onDelete(documentId);
    }

    return (
        <Card key={document.id} className="w-full shadow-lg">
            <>
                <CardHeader>
                    <CardTitle>
                        <Link to={`/documents/${document.id}`}>{document.title}</Link>
                    </CardTitle>
                </CardHeader>
                <CardContent>
                    <p className="text-gray-500/75">
                        {document.content.length > 100
                            ? `${document.content.slice(0, 100)}...`
                            : document.content}
                    </p>
                    <div className="flex mt-6">
                        <p className="text-sm bg-gray-200 px-2 py-1 rounded-full">
                            {document.studyTopic}
                        </p>
                    </div>
                </CardContent>
                <CardFooter>
                <p className="text-gray-500 mb-1">
                    {formatDateToISO(document.createdAt)}
                </p>
                </CardFooter>
                <DeleteDocumentModal onConfirm={() => handleDelete(document.id)} />
            </>
        </Card>
    );
};