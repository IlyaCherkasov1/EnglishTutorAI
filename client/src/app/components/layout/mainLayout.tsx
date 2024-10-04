import {Header} from "@/app/components/layout/header.tsx";
import {Outlet} from "react-router-dom";

export const MainLayout = () => {
    return (
        <>
            <Header />
            <Outlet />
        </>
    );
}