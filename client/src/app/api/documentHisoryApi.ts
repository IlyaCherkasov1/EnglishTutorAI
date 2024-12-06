import {Pageable} from "@/app/dataModels/common/pageable.ts";
import {MistakeHistoryItems} from "@/app/dataModels/mistakeHistoryItems.ts";
import {httpGet} from "@/app/infrastructure/requestApi.ts";
import {objectToQueryString} from "@/app/infrastructure/utils/paramsUtils.ts";
import {DocumentMistakeHistoryItems} from "@/app/dataModels/document/documentMistakeHistoryItems.ts";
import {SearchResult} from "@/app/dataModels/searchResult.ts";

const documentHistoryResource = "documentHistory";

export const getMistakeHistoryItems = async (request: Pageable): Promise<SearchResult<MistakeHistoryItems>> => {
    return httpGet({ url: `${documentHistoryResource}/get-mistake-history-items${objectToQueryString(request)}` })
}

export const getCurrentDocumentSessionMistakesHistory = async (userDocumentId: string): Promise<Array<DocumentMistakeHistoryItems>> => {
    return httpGet({url: `${documentHistoryResource}/get-session-mistake-history/${userDocumentId}` })
}