import {EmptyObject} from "react-hook-form";

export interface Result<T = EmptyObject> {
    data: T;
    isSucceeded: boolean;
    errors: string[];
}