import {z} from "zod";

export const AdminPanelSchema = z.object({
    title: z.string().min(3, {message: "Minimum 3 character is required"}),
    content: z.string().min(1, {message: "The field is required"}),
});

export type TAdminPanelSchema = z.infer<typeof AdminPanelSchema>;