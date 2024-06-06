'use client'

import React, {useRef} from "react";
import {SubmitButton} from "@/app/components/component/buttons/submitButton";
import {useI18n} from "@/app/locales/client";
import {addDocumentAction} from "@/app/actions/actions";
import {Textarea} from "@/app/components/ui/textarea";

export default function AdminPanel() {
    const t = useI18n();
    const formRef = useRef<HTMLFormElement>(null);

    return (
        <form ref={formRef} action={async (formData) => {
            await addDocumentAction(formData);
            formRef.current?.reset();
        }} className="space-y-4">
            <div className="mb-2 text-lg font-medium">{t('title')}</div>
            <input
                type="text"
                name="document-title"
                placeholder="Required"
                required
                className="block w-1/5 p-2 border border-gray-300 rounded"
            />
            <div className="mb-2 text-lg font-medium">{t('documentContent')}</div>
            <Textarea name="document-input" required />
            <SubmitButton />
        </form>
    );
}