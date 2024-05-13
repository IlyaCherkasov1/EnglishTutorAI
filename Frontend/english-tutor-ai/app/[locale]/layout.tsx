import "./globals.css";
import {AppRouterCacheProvider} from "@mui/material-nextjs/v13-appRouter";
import theme from "@/app/theme";
import {ThemeProvider} from "@mui/material";
import {ReactNode} from "react";
import { I18nProviderClient } from "../locales/client";

export default async function RootLayout({ children, params: { locale } }: Readonly<{
    children: ReactNode;
    params: { locale: string };
}>) {

    return (
        <html>
        <body>
        <AppRouterCacheProvider>
            <ThemeProvider theme={theme}>
                <I18nProviderClient locale={locale}>
                    {children}
                </I18nProviderClient>
            </ThemeProvider>
        </AppRouterCacheProvider>
        </body>
        </html>
    );
}