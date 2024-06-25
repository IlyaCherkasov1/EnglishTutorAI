export interface IdentityUserResponse {
    id: string;
    userName: string;
    email: string;
    emailConfirmed: boolean;
    twoFactorEnabled: boolean;
    lockoutEnabled: boolean;
    accessToken: string;
}