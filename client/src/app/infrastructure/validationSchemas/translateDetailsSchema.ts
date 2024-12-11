import {z} from "zod";
import {getLocalizedMessage} from "@/app/infrastructure/utils/localizerUtils.ts";

export const TranslateDetailsSchema = z.object({
    translatedText: z.string().min(1, { message: getLocalizedMessage('fieldRequired') }),
});

export type TypeTranslateDetailsSchema = z.infer<typeof TranslateDetailsSchema>;