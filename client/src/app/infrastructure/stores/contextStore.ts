import {makeAutoObservable} from "mobx";
import {ContextResponse} from "@/app/dataModels/contextResponse.ts";
import {userRoles} from "@/app/infrastructure/constants/userRoles.ts";

class ContextStore {
    public isAuthenticated = false;
    public firstName?: string;
    public isContextLoaded = false;
    public roleName?: Array<string>;
    public email?: string;
    public language: string = "ru";

    constructor() {
        makeAutoObservable(this)
    }

    public setContext(response: ContextResponse) {
        if (!this.isContextLoaded) {
            this.isContextLoaded = true;
        }

        this.isAuthenticated = response.isAuthenticated;
        this.firstName = response.firstName;
        this.roleName = response.roleName;
        this.email = response.email;
        this.language = response.language;
    }

    get isAdminRole(): boolean {
        return this.roleName?.includes(userRoles.admin) ?? false;
    }
}

export const contextStore = new ContextStore();