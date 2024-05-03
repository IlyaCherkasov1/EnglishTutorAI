import {DocumentResponse} from "@/app/dataModels/document/documentResponse";
import {RequestMethod} from "@/app/core/enum/requestMethod";
import {DocumentCreationRequest} from "@/app/dataModels/document/documentCreationRequest";
import {DocumentListItem} from "@/app/dataModels/document/documentListItem";

export const getDocumentByIndex = async ( index: number): Promise<DocumentResponse> => {
    const response = await fetch(`https://localhost:7008/api/Document/get-document-by-index/${index}`, {
        method: RequestMethod.GET,
    })

   return response.json();
}

export const getDocumentCount = async (): Promise<number> => {
    const response = await fetch("https://localhost:7008/api/Document/count", {
        method: RequestMethod.GET
    })

    return response.json();
}

export const addDocument = async (request: DocumentCreationRequest ) => {
    await fetch("https://localhost:7008/api/Document/add-document", {
        method: RequestMethod.POST,
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(request),
    })
}

export const getAllDocuments = async (): Promise<DocumentListItem[]> => {
    const response = await fetch(`https://localhost:7008/api/Document/get-documents`, {
        method: RequestMethod.GET,
    })

    return response.json();
}