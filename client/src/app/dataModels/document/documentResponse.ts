export interface DocumentResponse {
    id: string;
    title: string;
    content: string;
    createdAt: Date;
    threadId: string;
    currentLine: number;
}