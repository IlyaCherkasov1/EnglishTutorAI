export interface DocumentDetailsModel {
    userDocumentId: string;
    title: string;
    sentences: Array<string>;
    createdAt: Date;
    threadId: string;
    currentLine: number;
    isDocumentFinished: boolean;
}