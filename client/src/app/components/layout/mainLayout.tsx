import {Header} from "@/app/components/layout/header.tsx";
import {Outlet, useLocation} from "react-router-dom";
import {routes} from "@/app/components/layout/routes/routeLink.ts";

export const MainLayout = () => {
    const location = useLocation();
    const isHomeOrProfilePage = location.pathname === routes.translates || location.pathname === routes.profile;

    const marginBottom = isHomeOrProfilePage
        ? "mb-20"
        : "mb-24";

    return (
        <>
            <div className={marginBottom}>
                <Header />
            </div>
            <div className="max-w-7xl mx-auto">
                <Outlet />
            </div>
        </>
    );
}