import {DocumentResponse} from "@/app/dataModels/document/documentResponse";
import {DocumentCreationRequest} from "@/app/dataModels/document/documentCreationRequest";
import {DocumentListItem} from "@/app/dataModels/document/documentListItem";
import {SplitDocumentContentRequest} from "@/app/dataModels/splitDocumentContentRequest";
import {httpGet, httpPost} from "@/app/infrastructure/requestApi";
import {SaveCurrentLineRequest} from "@/app/dataModels/document/saveCurrentLineRequest";
import {ChatMessageResponse} from "@/app/dataModels/ChatMessageResponse";

const documentsResource = "document";

export const getDocumentByIndex = async ( index: number): Promise<DocumentResponse> => {
    return httpGet({ url: `${documentsResource}/count/get-document-by-index/${index}` })
}

export const getDocumentCount = async (): Promise<number> => {
    return httpGet({ url: `${documentsResource}/count` })
}

export const addDocument = async (request: DocumentCreationRequest ) => {
    return httpPost({
        url: `${documentsResource}/add-document`,
        body: request,
    });
}

export const getAllDocuments = async (): Promise<DocumentListItem[]> => {
    return httpGet({ url: `${documentsResource}/get-documents` })
}

export const getDocumentDetails = async (id: string): Promise<DocumentResponse> => {
    return httpGet({ url: `${documentsResource}/get-document-details/${id}` })
}

export const splitDocumentContent = async (request: SplitDocumentContentRequest): Promise<string[]> => {
    return httpPost({
        url: `${documentsResource}/split-document-content`,
        body: request,
    });
}

export const saveCurrentLine = async (request: SaveCurrentLineRequest): Promise<void> =>{
    return httpPost({
        url: `${documentsResource}/save-current-line`,
        body: request,
    })
}

export const getConversationThread = async (threadId: string): Promise<ChatMessageResponse[]> => {
    return httpGet({
        url: `${documentsResource}/get-conversation-thread/${threadId}`,
    })
}

export const deleteDocument = async (documentId: string): Promise<void> => {
    return httpPost({
        url: `${documentsResource}/delete-document/${documentId}`,
    })
}