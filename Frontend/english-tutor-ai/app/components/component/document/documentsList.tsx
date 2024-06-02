'use client'

import React, {useState} from 'react';
import {DocumentListItem} from "@/app/dataModels/document/documentListItem";
import {DocumentListItemComponent} from "@/app/components/component/document/documentListItemComponent";
import {useI18n} from "@/app/locales/client";

interface Props {
    allDocuments: DocumentListItem[];
}

export const DocumentsList = (props: Props) => {
    const [documents, setDocuments] = useState(props.allDocuments);
    const t = useI18n();

    const handleDeleteDocument = (documentId: string) => {
        setDocuments(documents.filter(document => document.id !== documentId));
    }

    return (
        <main>
            <div className="bg-gray-100">
                <ul className="divide-y divide-gray-200">
                    {documents ? (
                        documents.map(document => (
                            <DocumentListItemComponent
                                key={document.id}
                                document={document}
                                onDelete={handleDeleteDocument} />
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