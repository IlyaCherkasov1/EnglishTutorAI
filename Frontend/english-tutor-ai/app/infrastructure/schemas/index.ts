import * as z from "zod";

export const LoginSchema = z.object({
    email: z.string().email({
        message: "Invalid email address",
    }),
    password: z.string().min(1, {
        message: "Password is required",
    })
})

export type TLoginSchema = z.infer<typeof LoginSchema>

export const RegisterSchema = z.object({
    email: z.string().email({
        message: "Invalid email address",
    }),
    password: z.string().min(6, {
        message: "Minimum 6 characters required",
    }),
    userName: z.string().min(1, {
        message: "Name is required",
    }),
})

export type TRegisterSchema = z.infer<typeof RegisterSchema>