import {DocumentResponse} from "@/app/dataModels/document/documentResponse.ts";
import {DocumentCreationRequest} from "@/app/dataModels/document/documentCreationRequest.ts";
import {SplitDocumentContentRequest} from "@/app/dataModels/splitDocumentContentRequest.tsx";
import {SaveCurrentLineRequest} from "@/app/dataModels/document/saveCurrentLineRequest.ts";
import {ChatMessageResponse} from "@/app/dataModels/chatMessageResponse.ts";
import {httpGet, httpPost} from "@/app/infrastructure/requestApi.ts";
import {DocumentSearchRequest} from "@/app/dataModels/document/documentSearchRequest.ts";
import {Pageable} from "@/app/dataModels/common/pageable.ts";
import {objectToQueryString} from "@/app/infrastructure/utils/paramsUtils.ts";
import {MistakeHistoryItems} from "@/app/dataModels/mistakeHistoryItems.ts";
import {DocumentListItem} from "@/app/dataModels/document/documentListItem.ts";

const documentsResource = "document";

export const addDocument = async (request: DocumentCreationRequest) => {
    return httpPost({
        url: `${documentsResource}/add-document`,
        body: request,
    });
}

export const getDocuments = async (request: DocumentSearchRequest): Promise<Array<DocumentListItem>> => {
    return httpGet({ url: `${documentsResource}/get-documents${objectToQueryString(request)}` })
}

export const getMistakeHistoryItems = async (request: Pageable): Promise<Array<MistakeHistoryItems>> => {
    return httpGet({ url: `${documentsResource}/get-mistake-history-items${objectToQueryString(request)}` })
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