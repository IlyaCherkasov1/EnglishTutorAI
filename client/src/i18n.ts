import i18n from "i18next";
import {initReactI18next} from "react-i18next";
import en from '@/app/locales/en/translation.json';
import ru from '@/app/locales/ru/translation.json';
import LanguageDetector from "i18next-browser-languagedetector";

i18n
    .use(LanguageDetector)
    .use(initReactI18next)
    .init({
        resources: {
            en: { translation: en  },
            ru: { translation: ru  },
        },
        fallbackLng: 'en',
        interpolation: {
            escapeValue: false
        }
    })
    .catch((error) => console.error(error));

export default i18n;