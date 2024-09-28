import {useTranslation} from "react-i18next";
import {Button} from "@/app/components/ui/button.tsx";

export const GoogleSignInButton = () => {
    const { t } = useTranslation();

    const loginWithGoogle = async () => {
        try {
            const returnUrl = encodeURIComponent(import.meta.env.VITE_APP_CLIENT_URL);
            const provider = 'Google';
            const baseAppUrl = import.meta.env.VITE_APP_API_URL;
            window.location.href = `${baseAppUrl}/externalAuth/external-login?provider=${provider}&returnUrl=${returnUrl}`;
        } catch (error) {
            console.error('Error logging in with Google:', error);
        }
    };

    return (
        <Button type="button"
                onClick={loginWithGoogle}
                className="flex items-center justify-center w-full px-4 py-2 text-gray-700 bg-white border border-gray-300 rounded-md shadow-sm hover:bg-gray-100 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-indigo-500">
            <img src="/public/google-logo.svg"
                 alt="Google sign-in"
                 width="24"
                 height="24"/>
            <span className="ml-2">{t('signInWithGoogle')}</span>
        </Button>
    );
};