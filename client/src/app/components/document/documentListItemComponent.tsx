import {deleteDocument} from "@/app/api/documentApi.ts";
import {DocumentListItem} from "@/app/dataModels/document/documentListItem.ts";
import {Card, CardContent, CardFooter, CardHeader, CardTitle} from "@/app/components/ui/card.tsx";
import {formatDateToISO} from "@/app/infrastructure/helpers/dateHelpers.ts";
import {DeleteDocumentModal} from "@/app/components/modals/deleteDocumentModal.tsx";
import {Link} from "react-router-dom";
import {useTranslation} from "react-i18next";
import {contextStore} from "@/app/infrastructure/stores/contextStore.ts";

interface Props {
    document: DocumentListItem;
    onDelete: (documentId: string) => void;
}

export const DocumentListItemComponent = ({ document, onDelete }: Props) => {
    const { t } = useTranslation();

    const handleDelete = async (documentId: string) => {
        await deleteDocument(documentId);
        onDelete(documentId);
    }

    return (
        <Card key={document.id} className="w-80 h-80 shadow-lg">
            {document.isDocumentFinished && (
                <div className="absolute bottom-2 right-2 bg-badge text-badge-foreground text-xs
                        font-semibold px-2 py-1 rounded-full">
                    {t("completed")}
                </div>
            )}
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
                            {t(`studyTopics.${document.studyTopic}`)}
                        </p>
                    </div>
                </CardContent>
                <CardFooter>
                    <p className="text-gray-500 mb-1">
                        {formatDateToISO(document.createdAt)}
                    </p>
                </CardFooter>
                {contextStore.isAdminRole && (
                    <DeleteDocumentModal onConfirm={() => handleDelete(document.id)} />)}
            </>
        </Card>
    );
};