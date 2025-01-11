import {Pageable} from "@/app/dataModels/common/pageable.ts";

export interface TranslateSearchRequest extends Pageable {
    studyTopic: string;
}