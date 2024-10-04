import {useForm, SubmitHandler, FormProvider} from "react-hook-form";
import {useTranslation} from "react-i18next";
import {Textarea} from "@/app/components/ui/textarea.tsx";
import {addDocument} from "@/app/api/document/documentApi.ts";
import {Button} from "@/app/components/ui/button.tsx";
import {AdminPanelSchema, TAdminPanelSchema} from "@/app/infrastructure/zodSchemas/adminPanelSchema.ts";
import {zodResolver} from "@hookform/resolvers/zod";
import {Input} from "@/app/components/ui/input.tsx";
import {FormMessage} from "@/app/components/ui/form.tsx";

type FormValues = {
    title: string;
    content: string;
};

export const AdminPanel = () => {
    const { t } = useTranslation();

    const methods = useForm<TAdminPanelSchema>({
        resolver: zodResolver(AdminPanelSchema),
    });

    const { register, handleSubmit, reset, formState: { errors, isSubmitting } } = methods;

    const onSubmit: SubmitHandler<FormValues> = async (data) => {
        await addDocument({ title: data.title, content: data.content });
        reset();
    };

    return (
        <FormProvider {...methods}>
            <form onSubmit={handleSubmit(onSubmit)} className="space-y-4">
                <div className="mb-2 text-lg font-medium">{t('title')}</div>
                <Input
                    {...register("title")}
                    type="text"
                    placeholder="Required"
                    className={`block w-1/5 p-2 border ${errors.title ? 'border-red-500' : 'border-gray-300'} rounded`}
                    disabled={isSubmitting}
                />
                <FormMessage>{errors.title?.message}</FormMessage>
                <div className="mb-2 text-lg font-medium">{t('documentContent')}</div>
                <Textarea
                    {...register("content")}
                    className={`${errors.content ? 'border-red-500' : ''}`}
                    disabled={isSubmitting}
                />
                <FormMessage>{errors.content?.message}</FormMessage>
                <Button disabled={isSubmitting} type="submit">
                    {isSubmitting ? t('Submitting...') : t('Submit')}
                </Button>

                {isSubmitting &&
                    <div className="text-center mt-4">{t('loading')}...</div>} {/* Show loading indicator */}
            </form>
        </FormProvider>
    );
}