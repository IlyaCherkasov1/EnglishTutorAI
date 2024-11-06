import { useTranslation } from "react-i18next";
import { Link } from "react-router-dom";
import {routes} from "@/app/components/layout/routes/routeLink.ts";

export const AccessDenied = () => {
    const { t } = useTranslation();

    return (
        <div className="flex flex-col items-center justify-center h-screen bg-gray-100">
            <h1 className="text-3xl font-bold text-red-600 mb-4">
                {t('accessDenied')}
            </h1>
            <p className="text-lg text-gray-700 mb-8">
                {t('youDoNotHavePermission')}
            </p>
            <Link to={routes.home} className="text-blue-500 underline">
                {t('goBackHome')}
            </Link>
        </div>
    );
};