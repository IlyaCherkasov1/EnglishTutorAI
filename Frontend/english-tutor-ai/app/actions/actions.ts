'use server'

import {addDocument} from "@/app/api/document/documentApi";
import {correctText} from "@/app/api/languageModel/languageModelApi";

export const addDocumentAction = async (formData: FormData) => {
    const title = formData.get('document-title') as string;
    const content = formData.get('document-input') as string;

    await addDocument({ title: title, content: content });
}

export const handleCorrection = async (formData: FormData, currentLine: string): Promise<{ isCorrected: boolean, correctedText: string }>  => {
    const translatedText = formData.get('textarea-value') as string;

    const { isCorrected, correctedText } = await correctText({
        originalText: currentLine,
        translatedText: translatedText,
    })

    return { isCorrected, correctedText };
}