import NextAuth from "next-auth"
import Credentials from "next-auth/providers/credentials"
import {login} from "@/app/api/identity/identityApi";
import {LoginSchema} from "@/app/infrastructure/schemas";

export const { handlers, signIn, signOut, auth } = NextAuth({
    providers: [
        Credentials({
            credentials: {
                email: { label: "Email", type: "text" },
                password: { label: "Password", type: "password" }
            },
            authorize: async (credentials) => {
                const validatedFields = LoginSchema.safeParse(credentials);

                if (validatedFields.success) {
                    const { email, password } = validatedFields.data;

                    const loginResponse = await login({ email: email, password: password });

                    const userResponse = await fetch(`${process.env.NEXT_PUBLIC_LOCAL_API_URL}/user/get-user`, {
                        method: 'GET',
                        headers: {
                            'Content-Type': 'application/json',
                            'Authorization': `Bearer ${loginResponse.accessToken}`
                        },
                    });

                    const user = await userResponse.json();
                    user.accessToken = loginResponse.accessToken;

                    if (!user) {
                        throw new Error("User not found.")
                    }

                    return user;
                }

                return null;
            }
        }),
    ],
    pages: {
        signIn: '/auth/login',
    },
    callbacks: {
        async jwt({ token, user }) {
            if (user) {
                token.userName = user.userName;
                token.email = user.email;
                token.emailConfirmed = user.emailConfirmed;
                token.twoFactorEnabled = user.twoFactorEnabled;
                token.lockoutEnabled = user.lockoutEnabled;
                token.accessToken = user.accessToken;
            }
            return token;
        },
        async session({ session, token }) {
            if (token.sub && session.user){
                session.user.id = token.sub;
            }

            session.user.userName = token.userName as string;
            session.user.email = token.email as string;
            session.user.emailConfirmed = token.emailConfirmed as boolean;
            session.user.twoFactorEnabled = token.twoFactorEnabled as boolean;
            session.user.lockoutEnabled = token.lockoutEnabled as boolean;
            session.user.accessToken = token.accessToken as string;

            return session;
        },
    },
    secret: process.env.AUTH_SECRET,
})