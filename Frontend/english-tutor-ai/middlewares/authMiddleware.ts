import {type NextFetchEvent, type NextRequest, NextResponse} from 'next/server';
import {CustomMiddleware} from './chain';
import {auth} from '@/auth';
import {authRoutes, DEFAULT_LOGIN_REDIRECT_URI, LOGIN_PATH, publicRoutes} from '@/routes';
import {LOCALES} from '@/app/infrastructure/locales';

const removeLocaleFromPathname = (pathname: string, locales: string[]): string => {
    const localePattern = new RegExp(`^/(${locales.join('|')})`);
    return pathname.replace(localePattern, '');
}

export function authMiddleware(middleware: CustomMiddleware) {
    return async (request: NextRequest, event: NextFetchEvent) => {
        const session = await auth();
        const isLoggedIn = !!session?.user;
        const { pathname, origin } = request.nextUrl;
        const normalizedPathname = removeLocaleFromPathname(pathname, LOCALES);

         const isPublicRoute = publicRoutes.includes(normalizedPathname);
         const isAuthRoute = authRoutes.includes(normalizedPathname);

         if (isAuthRoute) {
             if (isLoggedIn) {
                 return NextResponse.redirect(new URL(DEFAULT_LOGIN_REDIRECT_URI, origin));
             }
         } else if (!isLoggedIn && !isPublicRoute) {
             return NextResponse.redirect(new URL(LOGIN_PATH, origin));
         }

        const response = NextResponse.next();
        return middleware(request, event, response);
    };
}