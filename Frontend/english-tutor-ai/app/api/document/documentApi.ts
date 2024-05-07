import {DocumentResponse} from "@/app/dataModels/document/documentResponse";
import {DocumentCreationRequest} from "@/app/dataModels/document/documentCreationRequest";
import {DocumentListItem} from "@/app/dataModels/document/documentListItem";
import {SplitDocumentContentRequest} from "@/app/dataModels/splitDocumentContentRequest";
import {httpGet, httpPost} from "@/app/core/requestApi";

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