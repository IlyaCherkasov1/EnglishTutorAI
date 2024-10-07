import {z} from "zod";
import {getLocalizedMessage} from "@/app/infrastructure/utils/localizerUtils.ts";

export const DocumentDetailsSchema = z.object({
    translatedText: z.string().min(1, { message: getLocalizedMessage('fieldRequired') }),
});

export type TDocumentDetailsSchema = z.infer<typeof DocumentDetailsSchema>;