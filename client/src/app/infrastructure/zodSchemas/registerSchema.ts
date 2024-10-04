import * as z from "zod";

export const RegisterSchema = z.object({
    email: z.string().email({
        message: "Invalid email address",
    }),
    password: z.string().min(6, {
        message: "Minimum 6 characters required",
    }),
    firstName: z.string().min(2, {
        message: "Minimum 2 characters required",
    }),
})

export type TRegisterSchema = z.infer<typeof RegisterSchema>