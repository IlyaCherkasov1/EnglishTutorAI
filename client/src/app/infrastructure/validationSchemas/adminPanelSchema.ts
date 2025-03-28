import {z} from "zod";
import {getLocalizedMessage} from "@/app/infrastructure/utils/localizerUtils.ts";
import {StudyTopic} from "@/app/dataModels/enums/studyTopic.ts";
import {getEnumValues} from "@/app/infrastructure/utils/enumUtils.ts";

export const AdminPanelSchema = z.object({
    title: z.string()
        .min(3, { message: getLocalizedMessage('minCharactersRequired', { count: 3 }) })
        .max(30, { message: getLocalizedMessage('maxCharactersAllowed', { count: 30 }) }),
    content: z.string().min(1, {
        message: getLocalizedMessage('fieldRequired')
    }),
    studyTopic: z.enum(getEnumValues(StudyTopic),
        {
            message: getLocalizedMessage('fieldRequired')
        }),
});

export type TAdminPanelSchema = z.infer<typeof AdminPanelSchema>;