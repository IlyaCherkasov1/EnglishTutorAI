'use server'

import {addDocument} from "@/app/api/document/documentApi";
import {correctText} from "@/app/api/languageModel/languageModelApi";
import {CreateAssistantResponse} from "@/app/dataModels/languageModel/createAssistantResponse";

export const addDocumentAction = async (formData: FormData) => {
    const title = formData.get('document-title') as string;
    const content = formData.get('document-input') as string;

    await addDocument({ title: title, content: content });
}

export interface handleCorrectionParams {
    formData: FormData;
    currentLine: string;
    createAssistantResponse: CreateAssistantResponse;
}

export const handleCorrection = async (props : handleCorrectionParams)
    : Promise<{ isCorrected: boolean, correctedText: string }> => {
    const translatedText = props.formData.get('textarea-value') as string;

    const { isCorrected, correctedText } = await correctText({
        originalText: props.currentLine,
        translatedText: translatedText,
        assistantId: props.createAssistantResponse.assistantId,
        threadId: props.createAssistantResponse.threadId,
    })

    return { isCorrected, correctedText };
}