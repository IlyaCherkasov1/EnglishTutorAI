export interface ContextResponse {
    isAuthenticated: boolean;
    firstName?: string;
    roleName: Array<string>;
    email?: string;
    language: string;
}