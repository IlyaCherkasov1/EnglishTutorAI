import {Pageable} from "@/app/dataModels/common/pageable.ts";
import {MistakeHistoryItems} from "@/app/dataModels/mistakeHistoryItems.ts";
import {httpGet} from "@/app/infrastructure/requestApi.ts";
import {objectToQueryString} from "@/app/infrastructure/utils/paramsUtils.ts";
import {TranslateMistakeHistoryItems} from "@/app/dataModels/translate/translateMistakeHistoryItems.ts";
import {SearchResult} from "@/app/dataModels/searchResult.ts";

const translateHistoryResource = "translateHistory";

export const getMistakeHistoryItems = async (request: Pageable): Promise<SearchResult<MistakeHistoryItems>> => {
    return httpGet({ url: `${translateHistoryResource}/get-mistake-history-items${objectToQueryString(request)}` })
}

export const getCurrentTranslateSessionMistakesHistory = async (userTranslateId: string): Promise<Array<TranslateMistakeHistoryItems>> => {
    return httpGet({url: `${translateHistoryResource}/get-session-mistake-history/${userTranslateId}` })
}