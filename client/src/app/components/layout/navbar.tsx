import {Link, useLocation} from "react-router-dom";
import {useTranslation} from "react-i18next";
import {routes} from "@/app/components/layout/routes/routeLink.ts";

export const Navbar = () => {
    const { t } = useTranslation();
    const location = useLocation();

    return (
        <div className="ml-4">
            <nav className="flex space-x-8">
                <Link to={routes.home}
                      className={`block py-2 ${location.pathname === routes.home ?
                          "text-gray-900 font-medium" : "text-gray-700 hover:text-black"}`}>
                    {t('home')}
                </Link>
                <Link to={routes.history}
                      className={`block py-2 ${location.pathname === routes.history ?
                          "text-gray-900 font-medium" : "text-gray-700 hover:text-black"}`}>
                    {t('history')}
                </Link>
                <Link to={routes.achievements}
                      className={`block py-2 ${location.pathname === routes.achievements ?
                          "text-gray-900 font-medium" : "text-gray-700 hover:text-black"}`}>
                    {t('achievementsLabel')}
                </Link>
            </nav>
        </div>
    );
};