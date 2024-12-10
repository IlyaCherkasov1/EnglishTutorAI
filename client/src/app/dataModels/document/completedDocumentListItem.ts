import {StudyTopic} from "@/app/dataModels/enums/studyTopic.ts";

export interface CompletedDocumentListItem {
    documentId: string;
    title: string;
    studyTopic: keyof typeof StudyTopic;
    content: string;
    completedOn: string;
}