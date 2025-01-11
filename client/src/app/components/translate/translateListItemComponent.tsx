import React from "react";
import { TranslateListItem } from "@/app/dataModels/translate/translateListItem.ts";
import { DeleteTranslateModal } from "@/app/components/modals/deleteTranslateModal.tsx";
import { Link } from "react-router-dom";
import { useTranslation } from "react-i18next";
import { contextStore } from "@/app/infrastructure/stores/contextStore.ts";
import {categoryColorSequences} from "@/app/infrastructure/constants/studyTopicColors.ts";

interface Props {
    translate: TranslateListItem;
    onDelete: (translateId: string) => void;
    index: number;
}

export const TranslateListItemComponent = React.memo(({ translate, onDelete, index }: Props) => {
    const { t } = useTranslation();

    const handleModalClick = (e: React.MouseEvent) => {
        e.stopPropagation();
        e.preventDefault();
    };

    const colorSequence = categoryColorSequences[translate.studyTopic];
    const cardColor = colorSequence[index % colorSequence.length];

    return (
        <Link to={`/translates/${translate.id}`} className="block relative w-[25rem]">
            <div
                key={translate.id}
                className={`relative rounded-2xl p-4 hover:shadow-lg transition-shadow border-2 ${cardColor}`}>
                <div className="flex items-center gap-3">
                    <div className="flex items-center justify-center w-9 h-9 rounded-full bg-white shadow">
                        <img
                            src={`/topics/${translate.studyTopic.toLowerCase()}.png`}
                            alt={`${translate.studyTopic} icon`}
                            className="w-5 h-5 object-contain"
                        />
                    </div>

                    <p className="text-sm font-medium text-gray-600">
                        {t(`studyTopics.${translate.studyTopic}`)}
                    </p>
                </div>

                <div className="flex mt-5">
                    <div>
                        <h3 className="text-lg font-semibold text-gray-800">
                            {translate.title}
                        </h3>
                    </div>
                    {contextStore.isAdminRole && (
                        <div onClick={handleModalClick}>
                            <DeleteTranslateModal onConfirm={() => onDelete(translate.id)} />
                        </div>
                    )}
                </div>
                <p className="text-gray-600 text-sm mt-3 line-clamp-3 min-h-16 mb-8">{translate.content}</p>
            </div>
        </Link>
    );
});