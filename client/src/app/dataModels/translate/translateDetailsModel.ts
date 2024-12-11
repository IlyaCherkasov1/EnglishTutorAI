export interface TranslateDetailsModel {
    userTranslateId: string;
    title: string;
    sentences: Array<string>;
    createdAt: Date;
    threadId: string;
    currentLine: number;
    isTranslateFinished: boolean;
}