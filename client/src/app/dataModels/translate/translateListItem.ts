import {StudyTopic} from "@/app/dataModels/enums/studyTopic.ts";

export interface TranslateListItem {
    id: string;
    title?: string | null;
    createdAt: string;
    content: string;
    studyTopic: keyof typeof StudyTopic;
}