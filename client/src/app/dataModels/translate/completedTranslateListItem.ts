import {StudyTopic} from "@/app/dataModels/enums/studyTopic.ts";

export interface CompletedTranslateListItem {
    translateId: string;
    title: string;
    studyTopic: keyof typeof StudyTopic;
    content: string;
    completedOn: string;
}