import * as z from "zod";
import {getLocalizedMessage} from "@/app/infrastructure/utils/localizerUtils.ts";

export const RegisterSchema = z.object({
    email: z.string().email({
        message: getLocalizedMessage('invalidEmailAddress')
    }),
    password: z.string().min(6, {
        message: getLocalizedMessage('minCharactersRequired', { count: 6 })
    }),
    firstName: z.string().min(2, {
        message: getLocalizedMessage('minCharactersRequired', { count: 2 })
    }),
})

export type TRegisterSchema = z.infer<typeof RegisterSchema>