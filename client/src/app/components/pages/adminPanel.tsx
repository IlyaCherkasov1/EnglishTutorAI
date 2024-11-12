import {useForm, FormProvider, Controller} from "react-hook-form";
import {useTranslation} from "react-i18next";
import {Textarea} from "@/app/components/ui/textarea.tsx";
import {addDocument} from "@/app/api/documentApi.ts";
import {Button} from "@/app/components/ui/button.tsx";
import {zodResolver} from "@hookform/resolvers/zod";
import {Input} from "@/app/components/ui/input.tsx";
import {FormMessage} from "@/app/components/ui/form.tsx";
import {AdminPanelSchema, TAdminPanelSchema} from "@/app/infrastructure/validationSchemas/adminPanelSchema.ts";
import {Select, SelectContent, SelectItem, SelectTrigger, SelectValue} from "@/app/components/ui/select.tsx";
import {getEnumValues} from "@/app/infrastructure/utils/enumUtils.ts";
import {StudyTopic} from "@/app/dataModels/enums/studyTopic.ts";
import {LoadingSpinner} from "@/app/components/ui/loadingSpinner.tsx";

export const AdminPanel = () => {
    const { t } = useTranslation();

    const methods = useForm<TAdminPanelSchema>({
        resolver: zodResolver(AdminPanelSchema),
        defaultValues: { studyTopic: "" },
    });

    const { register, handleSubmit, control, reset, formState: { errors, isSubmitting } } = methods;

    const onSubmit = async (values: TAdminPanelSchema) => {
        await addDocument({
            title: values.title,
            content: values.content,
            studyTopic: values.studyTopic,
        });

        reset();
    };

    const studyTopicOptions = getEnumValues(StudyTopic);

    return (
        <FormProvider {...methods}>
            <form onSubmit={handleSubmit(onSubmit)} className="space-y-4 ml-8">
                <div className="mb-2 text-lg font-medium">{t('title')}</div>
                <Input
                    {...register("title")}
                    type="text"
                    className="block w-1/5 p-2 border border-gray-300 rounded"
                    disabled={isSubmitting}
                />
                <FormMessage>{errors.title?.message}</FormMessage>
                <div className="mb-2 text-lg font-medium">{t('documentContent')}</div>
                <Textarea {...register("content")} className="w-1/5 h-32" disabled={isSubmitting} />
                <FormMessage>{errors.content?.message}</FormMessage>
                <div className="mb-2 text-lg font-medium">{t('category')}</div>
                <Controller
                    name="studyTopic"
                    control={control}
                    render={({ field }) => (
                        <Select value={field.value} onValueChange={field.onChange} disabled={isSubmitting}>
                            <SelectTrigger className="w-1/5">
                                <SelectValue placeholder={t('selectCategory')} />
                            </SelectTrigger>
                            <SelectContent>
                                {studyTopicOptions.map((topic) => (
                                    <SelectItem key={topic} value={topic}>
                                        {t(`studyTopics.${topic}`)}
                                    </SelectItem>
                                ))}
                            </SelectContent>
                        </Select>
                    )}
                />
                <FormMessage>{errors.studyTopic?.message}</FormMessage>
                <Button disabled={isSubmitting} type="submit">
                    {isSubmitting ? t('submitting...') : t('submit')}
                </Button>
                {isSubmitting && <LoadingSpinner />}
            </form>
        </FormProvider>
    );
}