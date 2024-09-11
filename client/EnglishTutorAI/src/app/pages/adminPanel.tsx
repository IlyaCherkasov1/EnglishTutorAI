import { useForm, SubmitHandler } from "react-hook-form";
import { useTranslation } from "react-i18next";
import { Textarea } from "../components/ui/textarea.tsx";
import { addDocument } from "../api/document/documentApi.ts";
import {Button} from "../components/ui/button.tsx";

type FormValues = {
    title: string;
    content: string;
};

export default function AdminPanel() {
    const { t } = useTranslation();
    const { register, handleSubmit, reset, formState: { errors, isSubmitting } } = useForm<FormValues>();

    const onSubmit: SubmitHandler<FormValues> = async (data) => {
        await addDocument({ title: data.title, content: data.content });
        reset();
    };

    return (
        <form onSubmit={handleSubmit(onSubmit)} className="space-y-4">
            <div className="mb-2 text-lg font-medium">{t('title')}</div>
            <input
                {...register("title", { required: true })}
                type="text"
                name="document-title"
                placeholder="Required"
                className={`block w-1/5 p-2 border ${errors.title ? 'border-red-500' : 'border-gray-300'} rounded`}
                disabled={isSubmitting}
            />
            {errors.title && <p className="text-red-500">This field is required</p>}

            <div className="mb-2 text-lg font-medium">{t('documentContent')}</div>
            <Textarea
                {...register("content", { required: true })}
                name="document-input"
                className={`${errors.content ? 'border-red-500' : ''}`}
                disabled={isSubmitting}
            />
            {errors.content && <p className="text-red-500">This field is required</p>}

            <Button disabled={isSubmitting} type="submit">
                {isSubmitting ? t('Submitting...') : t('Submit')}
            </Button>

            {isSubmitting && <div className="text-center mt-4">{t('loading')}...</div>} {/* Show loading indicator */}
        </form>
    );
}