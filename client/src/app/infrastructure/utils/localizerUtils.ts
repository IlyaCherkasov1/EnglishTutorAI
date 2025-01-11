import i18n from "@/i18n.ts";

export const getLocalizedMessage = (key: string, params?: Record<string, string | number>) => {
    let message = i18n.t(key);

    if (params) {
        Object.keys(params).forEach((param) => {
            const value = params[param];
            message = message.replace(`{{${param}}}`, String(value));
        });
    }

    return message;
};