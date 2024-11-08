export interface DocumentListItem {
    id: string;
    title?: string | null;
    createdAt: Date;
    content: string;
    studyTopic: string;
    isDocumentFinished: boolean;
}