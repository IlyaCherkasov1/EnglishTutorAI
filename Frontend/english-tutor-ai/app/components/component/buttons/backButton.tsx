'use client'

import React from 'react';
import {useRouter} from "next/navigation";
import {useI18n} from "@/app/locales/client";
import {Button} from "@/app/components/ui/button";
import { ArrowLeft } from 'lucide-react';

const BackButton = () => {
    const router = useRouter();
    const t = useI18n()

    return (
    <Button className="inline-flex items-center" variant="outline" onClick={() => router.back()}>
        <ArrowLeft className="mr-2 h-4 w-4" />
        {t('back')}
    </Button>
    );
};

export default BackButton;