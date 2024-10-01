import React, { useEffect, useState } from 'react';
import { useLocation, useNavigate } from 'react-router-dom';
import { isAuthPage } from '@/app/infrastructure/utils/authUtils.ts';
import { renewAccessTokenHandler } from '@/app/infrastructure/services/auth/identityService.ts';
import { responseHandlingStatuses } from '@/app/infrastructure/requestApi.ts';
import { routeLinks } from '@/app/components/layout/routes/routeLink.ts';
import { contextStore } from '@/app/infrastructure/stores/contextStore.ts';
import { contextService } from '@/app/infrastructure/services/contextService.ts';
import {observer} from "mobx-react-lite";

interface AuthGuardProps  {
    children: React.ReactNode;
}

export const AuthGuard: React.FC<AuthGuardProps > = observer(({ children }) => {
    const [loading, setLoading] = useState(true);
    const location = useLocation();
    const navigate = useNavigate();

    useEffect(() => {
        const loadContextAndRenewToken = async () => {
            try {
                if (isAuthPage(location.pathname)) {
                    return;
                }

                if (!contextStore.isContextLoaded) {
                    await contextService.load();
                }

                if (!contextStore.isAuthenticated) {
                    const result = await renewAccessTokenHandler();

                    if (result === responseHandlingStatuses.unauthenticated) {
                        navigate(routeLinks.login, { replace: true });
                    }
                }

            } catch (error) {
                console.error(error);
            } finally {
                setLoading(false);
            }
        };

        loadContextAndRenewToken().catch(console.error);
    }, [location.pathname, navigate]);

    if (loading) {
        return <div>Loading...</div>;
    }

    return <>{children}</>;
});