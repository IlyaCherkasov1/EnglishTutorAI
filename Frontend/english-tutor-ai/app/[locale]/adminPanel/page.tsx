'use client'

import {SubmitButton} from "@/app/[locale]/components/buttons/submitButton";
import {useI18n} from "@/app/locales/client";
import React, {useRef} from "react";
import {Textarea} from "@/app/[locale]/components/textarea-autosize/textArea";
import {TextField} from "@mui/material";
import Typography from '@mui/material/Typography/Typography';
import {addDocumentAction} from "@/app/actions/actions";

export default function AdminPanel() {
    const t = useI18n();
    const formRef = useRef<HTMLFormElement>(null);

    return (
        <form ref={formRef} action={async (formData) => {
            await addDocumentAction(formData);
            formRef.current?.reset();
        }}>
            <Typography>{t('title')}</Typography>
            <TextField sx={{ width: '20%' }} name="document-title" label="Required" variant="outlined" required />
            <Typography>{t('documentContent')}</Typography>
            <Textarea name="document-input" sx={{ width: '70%', marginBottom: 2 }}
                      aria-label="minimum height" minRows={1} required />
            <SubmitButton />
        </form>
    )
}