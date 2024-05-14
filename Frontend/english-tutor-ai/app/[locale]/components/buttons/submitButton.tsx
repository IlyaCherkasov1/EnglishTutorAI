'use client'

import {useFormStatus} from 'react-dom'
import {useI18n} from "@/app/locales/client";
import {Button} from "@mui/material";

export const SubmitButton = () => {
    const { pending } = useFormStatus()
    const t = useI18n()

    return (
        <Button type="submit" disabled={pending} variant="outlined">
            {pending ? t('addingDocumentStatus') : t('add')}
        </Button>
    )
}