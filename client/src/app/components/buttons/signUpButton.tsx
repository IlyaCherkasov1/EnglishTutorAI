import {useTranslation} from "react-i18next";
import { Button } from "@/app/components/ui/button.tsx";

interface Props {
    isSubmitting: boolean;
}

export const SignUpButton = ({ isSubmitting }: Props) => {
    const { t } = useTranslation();

    return (
        <Button className="w-full" type="submit" disabled={isSubmitting}>
            {isSubmitting ? t('signingUp') : t('signUp')}
        </Button>
    );
};