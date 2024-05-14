'use server'

import {addDocument} from "@/app/api/document/documentApi";

export const addDocumentAction = async (formData: FormData) => {
    const title = formData.get('document-title') as string;
    const content = formData.get('document-input') as string;

    await addDocument({ title: title, content: content });
}