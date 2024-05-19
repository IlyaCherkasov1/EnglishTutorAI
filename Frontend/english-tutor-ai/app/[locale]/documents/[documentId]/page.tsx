import React from 'react';
import {getDocumentDetails, splitDocumentContent} from "@/app/api/document/documentApi";
import BackButton from '@/app/components/component/buttons/backButton';
import {getI18n} from "@/app/locales/server";
import {DocumentDetail} from "@/app/components/component/documentDetail";

const DocumentDetails = async ({ params }: { params: { documentId: string } }) => {
    const document = await getDocumentDetails(params.documentId);
    const sentences = await splitDocumentContent({ text: document.content });
    const t = await getI18n()

    return document ? (
        <div className="flex flex-col h-screen">
            <div className="p-4">
                <BackButton/>
            </div>
            {sentences.length > 0 ? (
                <DocumentDetail documentTitle={document.title} sentences={sentences}/>
            ) : (
                <p>{t('loading')}</p>
            )}
        </div>
    ) : (
        <p>{t('loading')}</p>
    )
};

export default DocumentDetails;