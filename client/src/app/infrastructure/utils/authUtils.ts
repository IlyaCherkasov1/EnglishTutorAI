import {routes} from "@/app/components/layout/routes/routeLink.ts";

export const isAuthPage = (pathname: string) => {
    return pathname === routes.login || pathname === routes.register;
}

export const loginWithExternalProvider = async (provider: string) => {
    try {
        const returnUrl = encodeURIComponent(import.meta.env.VITE_APP_CLIENT_URL);
        const baseAppUrl = import.meta.env.VITE_APP_API_URL;
        window.location.href = `${baseAppUrl}/externalAuth/external-login?provider=${provider}&returnUrl=${returnUrl}`;
    } catch (error) {
        console.error(`Error logging in with ${provider}:`, error);
    }
};