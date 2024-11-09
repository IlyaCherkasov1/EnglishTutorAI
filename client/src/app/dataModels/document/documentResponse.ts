export interface DocumentResponse {
    id: string;
    title: string;
    sentences: Array<string>;
    createdAt: Date;
    threadId: string;
    currentLine: number;
    sessionId: string;
}