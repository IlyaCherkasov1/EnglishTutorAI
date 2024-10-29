import { ReactNode, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import { contextStore } from "@/app/infrastructure/stores/contextStore.ts";

interface Props {
    role: string;
    children: ReactNode;
}

export const RequireRole = ({ role, children }: Props) => {
    const navigate = useNavigate();

    useEffect(() => {
        if (!contextStore.roleName?.includes(role)) {
            navigate("/access-denied", { replace: true });
        }
    }, [role, navigate]);

    return <>{children}</>;
};