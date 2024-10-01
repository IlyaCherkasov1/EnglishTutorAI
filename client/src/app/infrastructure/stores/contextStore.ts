import {makeAutoObservable} from "mobx";
import {ContextResponse} from "@/app/dataModels/contextResponse.ts";

class ContextStore {
    public isAuthenticated = false;
    public firstName?: string;
    public isContextLoaded = false;

    constructor() {
        makeAutoObservable(this)
    }

    public setContext(response: ContextResponse) {
        if (!this.isContextLoaded) {
            this.isContextLoaded = true;
        }

        this.isAuthenticated = response.isAuthenticated;
        this.firstName = response.firstName;
    }
}

export const contextStore = new ContextStore();