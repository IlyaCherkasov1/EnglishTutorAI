import {
    type NextFetchEvent,
    type NextRequest
} from 'next/server'

import { CustomMiddleware } from './chain'
import {I18nMiddleware} from "@/app/infrastructure/locales";

export function localizationMiddleware(middleware: CustomMiddleware) {
    return async (request: NextRequest, event: NextFetchEvent) => {
        const i18nResponse = I18nMiddleware(request)
        return middleware(request, event, i18nResponse)
    }
}