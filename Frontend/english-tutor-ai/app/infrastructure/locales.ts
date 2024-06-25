import {createI18nMiddleware} from "next-international/middleware";

export const LOCALES = ['en', 'ru'];
export const DEFAULT_LOCALE = 'ru';

export const I18nMiddleware = createI18nMiddleware({
    locales: LOCALES,
    defaultLocale: DEFAULT_LOCALE
});