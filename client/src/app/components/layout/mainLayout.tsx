import {Header} from "@/app/components/layout/header.tsx";
import {Outlet} from "react-router-dom";

export const MainLayout = () => {
    return (
        <div className="max-w-5xl mx-auto">
            <Header />
            <Outlet />
        </div>
    );
}