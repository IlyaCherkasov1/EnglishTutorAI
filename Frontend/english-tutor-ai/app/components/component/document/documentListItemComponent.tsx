import React from 'react';
import {DocumentListItem} from "@/app/dataModels/document/documentListItem";
import {formatDateToISO} from "@/app/infrastructure/helpers/dateHelpers";
import {FileIcon} from "lucide-react";
import dynamic from "next/dynamic";
import {deleteDocument} from "@/app/api/document/documentApi";

interface Props {
    document: DocumentListItem;
    onDelete: (documentId: string) => void;
}

export const DocumentListItemComponent = ({ document, onDelete }: Props) => {
    const handleOnConfirm = async (documentId: string) => {
        await deleteDocument(documentId);
        onDelete(documentId);
    }

    const DeleteDocumentModal = dynamic(() => import('@/app/components/component/Modals/deleteDocumentModal')
        .then(mod => mod.DeleteDocumentModal), { ssr: false });

    return (
        <div className="w-full max-w mx-auto">
            <div className="bg-white rounded-lg shadow-md overflow-hidden">
                <ul className="divide-y divide-gray-200">
                    <li className="flex items-center py-3 px-4 hover:bg-gray-100 transition-colors duration-300">
                        <a href={`/documents/${document.id}`} className="flex-1 min-w-0 flex items-center no-underline">
                            <FileIcon className="h-4 w-4 text-gray-500 mr-2"/>
                            <div className="flex-1 min-w-0">
                                <p className="text-sm font-medium text-gray-900 truncate">{document.title}</p>
                                <p className="text-xs text-gray-500 truncate">{formatDateToISO(document.createdAt)}</p>
                            </div>
                        </a>
                        <DeleteDocumentModal onConfirm={() => handleOnConfirm(document.id)}/>
                    </li>
                </ul>
            </div>
        </div>
    );
};