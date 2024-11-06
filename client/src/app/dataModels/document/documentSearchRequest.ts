import {Pageable} from "@/app/dataModels/common/pageable.ts";

export interface DocumentSearchRequest extends Pageable {
    studyTopic: string;
}