import {StudyTopic} from "@/app/dataModels/enums/studyTopic.ts";

export interface DocumentListItem {
    id: string;
    title?: string | null;
    createdAt: Date;
    content: string;
    studyTopic: keyof typeof StudyTopic;
    isDocumentFinished: boolean;
}