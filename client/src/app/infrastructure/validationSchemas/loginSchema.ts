import * as z from "zod";
import {getLocalizedMessage} from "@/app/infrastructure/utils/localizerUtils.ts";

export const LoginSchema = z.object({
    email: z.string().email({
        message: getLocalizedMessage('invalidEmailAddress')
    }),
    password: z.string().min(1, {
        message: getLocalizedMessage('fieldRequired')
    })
})

export type TLoginSchema = z.infer<typeof LoginSchema>