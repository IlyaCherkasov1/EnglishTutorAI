import {z} from "zod";

export const DocumentDetailsSchema = z.object({
    translatedText: z.string().min(1, { message: "Translated text is required" }),
});

export type TDocumentDetailsSchema = z.infer<typeof DocumentDetailsSchema>;