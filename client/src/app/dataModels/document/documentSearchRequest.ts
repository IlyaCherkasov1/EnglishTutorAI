import {DocumentFilteredData} from "@/app/dataModels/document/documentFilteredData.ts";
import {Pageable} from "@/app/dataModels/common/pageable.ts";

export type DocumentSearchRequest = DocumentFilteredData & Pageable;