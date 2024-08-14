import NextAuth from "next-auth"
import Credentials from "next-auth/providers/credentials"
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

                    const response = await fetch(`${process.env.NEXT_PUBLIC_LOCAL_API_URL}/identity/login`, {
                        method: 'POST',
                        headers: {
                            'Content-Type': 'application/json',
                        },
                        body: JSON.stringify({ email: email, password: password })
                    });

                    if (!response.ok) {
                        throw new Error("Not allowed.")
                    }

                    return await response.json();
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
                token.accessToken = user.accessToken;
            }
            return token;
        },
        async session({ session, token }) {
            if (token.sub && session.user) {
                session.user.id = token.sub;
            }
            session.user.accessToken = token.accessToken as string;

            return session;
        },
    },
    secret: process.env.AUTH_SECRET,
})