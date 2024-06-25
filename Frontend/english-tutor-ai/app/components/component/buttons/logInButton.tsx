import {Button} from "@/app/components/ui/button";
import React from 'react';
import {useI18n} from "@/app/locales/client";

interface Props {
    isSubmitting: boolean;
}

export const LogInButton = ({ isSubmitting }: Props) => {
    const t = useI18n();

    return (
        <Button className="w-full" type="submit" disabled={isSubmitting}>
            {isSubmitting ? t('loggingIn') : t('logIn')}
        </Button>
    );
};