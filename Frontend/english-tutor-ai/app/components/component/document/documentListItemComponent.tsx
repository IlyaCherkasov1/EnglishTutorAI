import React from 'react';
import { DocumentListItem } from "@/app/dataModels/document/documentListItem";
import { formatDateToISO } from "@/app/core/helpers/dateHelpers";
import {FileIcon, Trash2} from "lucide-react";

interface Props {
    document: DocumentListItem;
}

export const DocumentListItemComponent = ({ document }: Props) => {
    return (
        <div className="w-full max-w mx-auto">
            <div className="bg-white rounded-lg shadow-md overflow-hidden">
                <ul className="divide-y divide-gray-200">
                    <li className="flex items-center py-3 px-4 hover:bg-gray-100 transition-colors duration-300">
                        <FileIcon className="h-4 w-4 text-gray-500 mr-2" />
                        <div className="flex-1 min-w-0">
                            <p className="text-sm font-medium text-gray-900 truncate">{document.title}</p>
                            <p className="text-xs text-gray-500 truncate">{formatDateToISO(document.createdAt)}</p>
                        </div>
                        <button
                            className="h-6 w-6 flex items-center justify-center text-gray-400 hover:text-red-500 transition-colors duration-300"
                            aria-label="delete">
                            <Trash2 className="h-4 w-4" />
                        </button>
                    </li>
                </ul>
            </div>
        </div>
    );
};