'use client'

import React from 'react';
import Button from '@mui/material/Button';
import ArrowBackIcon from '@mui/icons-material/ArrowBack';
import {useRouter} from "next/navigation";
import {useI18n} from "@/app/locales/client";

const BackButton = () => {
    const router = useRouter();
    const t = useI18n()

    return (
        <Button
            startIcon={<ArrowBackIcon />}
            onClick={() => router.back()}
            variant="outlined"
        >
            {t('back')}
        </Button>
    );
};

export default BackButton;