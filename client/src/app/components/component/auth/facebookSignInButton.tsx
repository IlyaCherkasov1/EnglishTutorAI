import {useTranslation} from "react-i18next";
import {Button} from "@/app/components/ui/button.tsx";
import {loginWithExternalProvider} from "@/app/infrastructure/utils/authUtils.ts";

export const FacebookSignInButton = () => {
    const { t } = useTranslation();

    return (
        <Button type="button"
                onClick={() => loginWithExternalProvider('Facebook')}
                className="flex items-center justify-center w-full px-4 py-2 text-gray-700 bg-white border border-gray-300 rounded-md shadow-sm hover:bg-gray-100 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-indigo-500">
            <img src="/public/facebook_logo.svg"
                 alt="Google sign-in"
                 width="24"
                 height="24"/>
            <span className="ml-2">{t('signInWith.Facebook')}</span>
        </Button>
    );
}