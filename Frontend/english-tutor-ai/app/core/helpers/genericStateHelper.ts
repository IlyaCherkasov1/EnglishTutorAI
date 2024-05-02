import {Dispatch, SetStateAction} from "react";

export type SetGenericStateType<T> = Dispatch<SetStateAction<T>>;