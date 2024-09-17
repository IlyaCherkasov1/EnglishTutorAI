import {useEffect, useState} from "react";
import {BrowserRouter} from "react-router-dom";
import App from "../../../app.tsx";
import {refreshToken} from "../services/auth/identityService.ts";

export const MainComponent = () => {
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        const fetchAccessToken = async () => {
            try {
                await refreshToken();
            } catch (error) {
                console.error('Error renewing access token:', error);
            } finally {
                setLoading(false);
            }
        };

        fetchAccessToken().catch(console.error);
    }, []);

    if (loading) {
        return null;
    }

    return (
        <BrowserRouter>
            <App />
        </BrowserRouter>
    );
};