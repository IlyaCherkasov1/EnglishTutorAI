import { Link, useLocation } from "react-router-dom";
import { useTranslation } from "react-i18next";
import { routes } from "@/app/components/layout/routes/routeLink.ts";
import {contextStore} from "@/app/infrastructure/stores/contextStore.ts";

export const Navbar = () => {
    const { t } = useTranslation();
    const location = useLocation();

    const navLinks = [
        { to: routes.home, label: t("home") },
        { to: routes.history, label: t("history") },
        { to: routes.profile, label: t("profile") },
    ];

    if (contextStore.isAdminRole){
        navLinks.push({to: routes.adminPanel, label: t('adminPanel')})
    }

    return (
        <nav className="flex space-x-10 px-7 text-2xl">
            {navLinks.map((link) => (
                <Link
                    key={link.to}
                    to={link.to}
                    className={`block py-2 ${
                        location.pathname === link.to
                            ? "text-black font-bold border-b-2 border-black"
                            : "text-gray-500 hover:text-black hover:font-bold"
                    }`}>
                    {link.label}
                </Link>
            ))}
        </nav>
    );
};