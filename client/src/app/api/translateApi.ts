import {TranslateDetailsModel} from "@/app/dataModels/translate/translateDetailsModel.ts";
import {TranslateCreationRequest} from "@/app/dataModels/translate/translateCreationRequest.ts";
import {SaveCurrentLineRequest} from "@/app/dataModels/translate/saveCurrentLineRequest.ts";
import {ChatMessageResponse} from "@/app/dataModels/chatMessageResponse.ts";
import {httpGet, httpPost} from "@/app/infrastructure/requestApi.ts";
import {TranslateSearchRequest} from "@/app/dataModels/translate/translateSearchRequest.ts";
import {objectToQueryString} from "@/app/infrastructure/utils/paramsUtils.ts";
import {TranslateListItem} from "@/app/dataModels/translate/translateListItem.ts";
import {SearchResult} from "@/app/dataModels/searchResult.ts";
import {NextTranslateSearchModel} from "@/app/dataModels/nextTranslateSearchModel.ts";
import {Pageable} from "@/app/dataModels/common/pageable.ts";
import {CompletedTranslateListItem} from "@/app/dataModels/translate/completedTranslateListItem.ts";

const translatesResource = "translate";

export const addTranslate = async (request: TranslateCreationRequest) => {
    return httpPost({
        url: `${translatesResource}/add-translate`,
        body: request,
    });
}

export const getTranslates = async (request: TranslateSearchRequest): Promise<SearchResult<TranslateListItem>> => {
    return httpGet({ url: `${translatesResource}/get-translates${objectToQueryString(request)}` })
}

export const getTranslateDetails = async (id: string): Promise<TranslateDetailsModel> => {
    return httpGet({ url: `${translatesResource}/get-translate-details/${id}` })
}

export const saveCurrentLine = async (request: SaveCurrentLineRequest): Promise<void> =>{
    return httpPost({
        url: `${translatesResource}/save-current-line`,
        body: request,
    })
}

export const getConversationThread = async (threadId: string): Promise<ChatMessageResponse[]> => {
    return httpGet({
        url: `${translatesResource}/get-conversation-thread/${threadId}`,
    })
}

export const deleteTranslate = async (translateId: string): Promise<void> => {
    return httpPost({
        url: `${translatesResource}/delete-translate/${translateId}`,
    })
}

export const handleTranslateCompletion = async (userTranslateId: string): Promise<void> => {
    return httpPost({ url: `${translatesResource}/handle-translate-completion/${userTranslateId}` });
}

export const handleTranslateStart = async (userTranslateId: string): Promise<void> => {
    return httpPost({ url: `${translatesResource}/handle-translate-start/${userTranslateId}`})
}

export const getNextTranslate = async (request: NextTranslateSearchModel): Promise<TranslateListItem | null> => {
    return httpGet({url: `${translatesResource}/get-next-translate${objectToQueryString(request)}`})
}

export const getCompletedTranslates = async (request: Pageable): Promise<SearchResult<CompletedTranslateListItem>> => {
    return httpGet({url: `${translatesResource}/get-completed-translates${objectToQueryString(request)}`})
}