// noinspection ES6UnusedImports
import NextAuth, { DefaultSession, DefaultUser } from "next-auth";

export type ExtendedUser = DefaultSession["user"] & {
    id: string;
    userName: string;
    email: string;
    emailConfirmed: boolean;
    twoFactorEnabled: boolean;
    lockoutEnabled: boolean;
}

declare module "next-auth" {
    interface Session {
        user: ExtendedUser;
    }

    interface User {
        id: string;
        tokenType: string;
        accessToken: string;
        expiresIn: number;
        refreshToken: string;
    }
}