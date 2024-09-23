import {useEffect, useState} from "react";
import {useLocation, useNavigate} from "react-router-dom";
import App from "../../../app.tsx";
import {renewAccessTokenHandler} from "../services/auth/identityService.ts";
import {isAuthPage} from "@/app/infrastructure/utils/authUtils.ts";
import {isAccessTokenValid} from "@/app/infrastructure/utils/tokenUtils.ts";

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
                    await renewAccessTokenHandler();
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