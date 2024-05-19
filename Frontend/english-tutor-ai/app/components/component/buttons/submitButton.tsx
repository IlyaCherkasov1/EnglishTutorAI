'use client'

import {useFormStatus} from 'react-dom';
import {useI18n} from "@/app/locales/client";

export const SubmitButton = () => {
    const { pending } = useFormStatus();
    const t = useI18n();

    return (
        <button
            type="submit"
            disabled={pending}
            className={
                `px-4 py-2 border rounded ${pending ? 'border-gray-400 text-gray-400' : 'border-blue-500 text-blue-500'}`}>
            {pending ? t('addingDocumentStatus') : t('add')}
        </button>
    );
};