export interface ContextResponse {
    userId: string;
    isAuthenticated: boolean;
    firstName?: string;
    roleName: Array<string>;
}