export interface IdentityUserResponse {
    id: string;
    firstName: string;
    email: string;
    emailConfirmed: boolean;
    twoFactorEnabled: boolean;
    lockoutEnabled: boolean;
    accessToken: string;
}