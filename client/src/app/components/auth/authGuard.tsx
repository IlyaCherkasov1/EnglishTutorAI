import React, {useState} from 'react';
import {useLocation, useNavigate} from 'react-router-dom';
import {isAuthPage} from '@/app/infrastructure/utils/authUtils.ts';
import {renewAccessTokenHandler} from '@/app/infrastructure/services/auth/identityService.ts';
import {responseHandlingStatuses} from '@/app/infrastructure/requestApi.ts';
import {routeLinks} from '@/app/components/layout/routes/routeLink.ts';
import {contextStore} from '@/app/infrastructure/stores/contextStore.ts';
import {contextService} from '@/app/infrastructure/services/contextService.ts';
import {observer} from "mobx-react-lite";
import useAsyncEffect from "use-async-effect";
import {LoadingSpinner} from "@/app/components/ui/loadingSpinner.tsx";

interface AuthGuardProps {
    children: React.ReactNode;
}

export const AuthGuard: React.FC<AuthGuardProps> = observer(({ children }) => {
    const [loading, setLoading] = useState(true);
    const location = useLocation();
    const navigate = useNavigate();

    useAsyncEffect(async () => {
        try {
            if (isAuthPage(location.pathname)) {
                return;
            }

            if (!contextStore.isAuthenticated) {
                const result = await renewAccessTokenHandler();

                if (result === responseHandlingStatuses.unauthenticated) {
                    navigate(routeLinks.login, { replace: true });
                }
            }

            if (!contextStore.isContextLoaded) {
                await contextService.load();
            }

        } catch (error) {
            console.error(error);
        } finally {
            setLoading(false);
        }
    }, [location.pathname, navigate]);

    if (loading) {
        return <LoadingSpinner />;
    }

    return <>{children}</>;
});