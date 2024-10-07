import {z} from "zod";
import {getLocalizedMessage} from "@/app/infrastructure/utils/localizerUtils.ts";

export const AdminPanelSchema = z.object({
    title: z.string().min(3, {
        message: getLocalizedMessage('minCharactersRequired', { count: 3 })
    }),
    content: z.string().min(1, {
        message: getLocalizedMessage('fieldRequired')
    }),
});

export type TAdminPanelSchema = z.infer<typeof AdminPanelSchema>;