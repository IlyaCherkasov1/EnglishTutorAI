'use server'

import {LoginSchema, RegisterSchema, TLoginSchema} from "@/app/infrastructure/schemas";
import {signIn} from "@/auth";
import {register} from "@/app/api/identity/identityApi";

export const loginAction = async (values: TLoginSchema) => {
    const validatedFields =  LoginSchema.safeParse(values);

    if (!validatedFields.success) {
        return { error: "Invalid fields!" };
    }

    const { email, password } = validatedFields.data;

    try {
        await signIn("credentials", {
            email, password, redirect: false,
        });

    } catch (error) {
        return { error: "Something went wrong." };
    }
}

export const RegisterAction = async (values: TLoginSchema) => {
    const validatedFields = RegisterSchema.safeParse(values);

    if (!validatedFields.success) {
        return { error: "Invalid fields!" };
    }

    const { userName, email, password } = validatedFields.data;

    try {
        const response = await register({ firstName: userName, email, password });

        if (!response.ok) {
            const responseData = await response.json();
            const errors = responseData.errors || [];

            if ('DuplicateEmail' in errors) {
                return { error: errors.DuplicateEmail[0] };
            } else {
                return { error: "Registration failed. Please try again." };
            }
        } else {
            return { success: "Registration was successful! Check your email" };
        }
    } catch (error) {
        if (error instanceof Error) {
            return { error: "Something went wrong. Please try again." };
        }
    }
};