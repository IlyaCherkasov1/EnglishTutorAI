import {Button} from "@/app/components/ui/button.tsx";
import {useTranslation} from "react-i18next";

interface Props {
    isSubmitting: boolean;
}

export const SignInButton = ({ isSubmitting }: Props) => {
    const { t } = useTranslation();

    return (
        <Button className="w-full" type="submit" disabled={isSubmitting}>
            {isSubmitting ? t('signingIn') : t('signIn')}
        </Button>
    );
};