import {useEffect, useState} from "react";
import {useLocation, useNavigate} from "react-router-dom";
import App from "@/app.tsx";
import {renewAccessTokenHandler} from "@/app/infrastructure/services/auth/identityService.ts";
import {isAccessTokenValid} from "@/app/infrastructure/utils/tokenUtils.ts";
import {responseHandlingStatuses} from "@/app/infrastructure/requestApi.ts";
import {routeLinks} from "@/app/components/layout/routes/routeLink.ts";
import {isAuthPage} from "@/app/infrastructure/utils/authUtils.ts";

export const MainComponent = () => {
    const [loading, setLoading] = useState(true);
    const location = useLocation();
    const navigate = useNavigate();

    useEffect(() => {
        const renewAccessToken = async () => {
            try {
                if (isAuthPage(location.pathname)) {
                    return;
                }

                if (!isAccessTokenValid()) {
                   const result = await renewAccessTokenHandler();

                   if (result == responseHandlingStatuses.unauthenticated){
                       navigate(routeLinks.login, { replace: true });
                   }
                }

            } catch (error) {
                console.error('Error renewing access token:', error);
            } finally {
                setLoading(false);
            }
        };

        renewAccessToken().catch(console.error);
    }, [location.pathname, navigate]);

    if (loading) {
        return null;
    }

    return <App/>;
};