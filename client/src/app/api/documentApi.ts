import {DocumentDetailsModel} from "@/app/dataModels/document/documentDetailsModel.ts";
import {DocumentCreationRequest} from "@/app/dataModels/document/documentCreationRequest.ts";
import {SaveCurrentLineRequest} from "@/app/dataModels/document/saveCurrentLineRequest.ts";
import {ChatMessageResponse} from "@/app/dataModels/chatMessageResponse.ts";
import {httpGet, httpPost} from "@/app/infrastructure/requestApi.ts";
import {DocumentSearchRequest} from "@/app/dataModels/document/documentSearchRequest.ts";
import {objectToQueryString} from "@/app/infrastructure/utils/paramsUtils.ts";
import {DocumentListItem} from "@/app/dataModels/document/documentListItem.ts";
import {SearchResult} from "@/app/dataModels/searchResult.ts";
import {NextDocumentSearchModel} from "@/app/dataModels/nextDocumentSearchModel.ts";
import {Pageable} from "@/app/dataModels/common/pageable.ts";
import {CompletedDocumentListItem} from "@/app/dataModels/document/completedDocumentListItem.ts";

const documentsResource = "document";

export const addDocument = async (request: DocumentCreationRequest) => {
    return httpPost({
        url: `${documentsResource}/add-document`,
        body: request,
    });
}

export const getDocuments = async (request: DocumentSearchRequest): Promise<SearchResult<DocumentListItem>> => {
    return httpGet({ url: `${documentsResource}/get-documents${objectToQueryString(request)}` })
}

export const getDocumentDetails = async (id: string): Promise<DocumentDetailsModel> => {
    return httpGet({ url: `${documentsResource}/get-document-details/${id}` })
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

export const handleDocumentCompletion = async (userDocumentId: string): Promise<void> => {
    return httpPost({ url: `${documentsResource}/handle-document-completion/${userDocumentId}` });
}

export const handleDocumentStart = async (userDocumentId: string): Promise<void> => {
    return httpPost({ url: `${documentsResource}/handle-document-start/${userDocumentId}`})
}

export const getNextDocument = async (request: NextDocumentSearchModel): Promise<DocumentListItem | null> => {
    return httpGet({url: `${documentsResource}/get-next-document${objectToQueryString(request)}`})
}

export const getCompletedDocuments = async (request: Pageable): Promise<SearchResult<CompletedDocumentListItem>> => {
    return httpGet({url: `${documentsResource}/get-completed-documents${objectToQueryString(request)}`})
}