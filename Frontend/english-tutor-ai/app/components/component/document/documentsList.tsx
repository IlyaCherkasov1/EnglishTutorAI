'use client'

import React from 'react';
import { DocumentListItem } from "@/app/dataModels/document/documentListItem";
import { DocumentListItemComponent } from "@/app/components/component/document/documentListItemComponent";
import { useI18n } from "@/app/locales/client";

interface Props {
    allDocuments: DocumentListItem[];
}

export const DocumentsList = (props: Props) => {
    const t = useI18n();

    return (
        <main>
            <div className="bg-gray-100">
                <ul className="divide-y divide-gray-200">
                    {props.allDocuments ? (
                        props.allDocuments.map(document => (
                            <a href={`/documents/${document.id}`} key={document.id} className="block no-underline">
                                <DocumentListItemComponent document={document} />
                            </a>
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