import {DocumentListItem} from "@/app/dataModels/document/documentListItem.ts";

export interface DocumentSearchResult {
    totalCount: number;
    items: Array<DocumentListItem> | null;
}