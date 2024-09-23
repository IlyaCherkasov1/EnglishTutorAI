import {Header} from "@/app/components/component/header.tsx";
import {Outlet} from "react-router-dom";

export const MainLayout = () => {
    return (
        <>
            <Header />
            <Outlet />
        </>
    );
}