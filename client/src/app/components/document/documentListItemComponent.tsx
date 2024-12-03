import React from "react";
import { DocumentListItem } from "@/app/dataModels/document/documentListItem.ts";
import { formatDateToISO } from "@/app/infrastructure/helpers/dateHelpers.ts";
import { DeleteDocumentModal } from "@/app/components/modals/deleteDocumentModal.tsx";
import { Link } from "react-router-dom";
import { useTranslation } from "react-i18next";
import { contextStore } from "@/app/infrastructure/stores/contextStore.ts";
import {studyTopicColors} from "@/app/infrastructure/constants/studyTopicColors.ts";

interface Props {
    document: DocumentListItem;
    onDelete: (documentId: string) => void;
}

export const DocumentListItemComponent = React.memo(({ document, onDelete }: Props) => {
    const { t } = useTranslation();

    const handleModalClick = (e: React.MouseEvent) => {
        e.stopPropagation();
        e.preventDefault();
    };

    const topicColor = studyTopicColors[document.studyTopic];

    return (
        <Link to={`/documents/${document.id}`} className="block relative">
            <div
                key={document.id}
                className={`relative rounded-2xl p-4 ${topicColor} hover:shadow-lg transition-shadow`}>
                <div className="flex items-center gap-3">
                    <div className="flex items-center justify-center w-9 h-9 rounded-full bg-white shadow">
                        <img
                            src={`/public/topics/${document.studyTopic.toLowerCase()}.png`}
                            alt={`${document.studyTopic} icon`}
                            className="w-5 h-5 object-contain"
                        />
                    </div>

                    <p className="text-sm font-medium text-gray-600">
                        {t(`studyTopics.${document.studyTopic}`)}
                    </p>
                </div>

                <div className="flex mt-5">
                    <div>
                        <h3 className="text-lg font-semibold text-gray-800">
                            {document.title}
                        </h3>
                    </div>
                    {contextStore.isAdminRole && (
                        <div onClick={handleModalClick}>
                            <DeleteDocumentModal onConfirm={() => onDelete(document.id)} />
                        </div>
                    )}
                </div>
                <p className="text-gray-600 text-sm mt-3 line-clamp-3 min-h-16">{document.content}</p>
                <div className="flex justify-between items-center mt-4">
                    <p className="text-sm text-gray-500">{formatDateToISO(document.createdAt)}</p>
                    {document.isDocumentFinished && (
                        <span className="text-xs font-semibold text-green-600 bg-green-100 px-2 py-1 rounded-full">
                            {t("completed")}
                        </span>
                    )}
                </div>
            </div>
        </Link>
    );
});