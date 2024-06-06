'use client'

import {useFormStatus} from 'react-dom';
import {useI18n} from "@/app/locales/client";
import {Button} from "@/app/components/ui/button";
import React from "react";

export const SendButton = () => {
    const { pending } = useFormStatus();
    const t = useI18n();

    return (
        <Button type="submit" disabled={pending}>{pending ? t('sending') : t('send')}</Button>
    )
};