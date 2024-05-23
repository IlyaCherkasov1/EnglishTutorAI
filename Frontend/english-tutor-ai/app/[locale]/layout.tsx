import "../globals.css";
import {ReactNode} from "react";
import {I18nProviderClient} from "../locales/client";

export default async function RootLayout({ children, params: { locale } }: Readonly<{
    children: ReactNode;
    params: { locale: string };
}>) {

    return (
        <html lang={locale}>
        <body>
        <I18nProviderClient locale={locale}>
            {children}
        </I18nProviderClient>
        </body>
        </html>
    );
}