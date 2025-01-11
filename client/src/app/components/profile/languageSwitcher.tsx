import {useTranslation} from "react-i18next";
import {Select, SelectContent, SelectItem, SelectTrigger, SelectValue} from "@/app/components/ui/select";
import {Language} from "@/app/dataModels/enums/language.ts";
import {useState} from "react";
import {getEnumValues} from "@/app/infrastructure/utils/enumUtils.ts";
import {changeUserLanguage} from "@/app/api/userApi.ts";
import {contextStore} from "@/app/infrastructure/stores/contextStore.ts";
import {languageMap} from "@/app/dataModels/languageMap.ts";

export const LanguageSwitcher = () => {
    const { t, i18n } = useTranslation();
    const [currentLanguage, setCurrentLanguage] = useState<string>(contextStore.language);

    const languages = getEnumValues(Language);

    const onSubmit = async (language: string) => {
        await changeUserLanguage({ language: language })
        setCurrentLanguage(language);
        contextStore.language = language;
        await i18n.changeLanguage(languageMap[language]);
    };

    return (
        <>
            <div className="mb-3 text-lg font-medium">{t('appLanguage')}</div>
            <Select value={currentLanguage} onValueChange={(value) => onSubmit(value)}>
                <SelectTrigger>
                    <SelectValue placeholder={t('selectCategory')} />
                </SelectTrigger>
                <SelectContent>
                    {languages.map((language) => (
                        <SelectItem key={language} value={language}>
                            {language}
                        </SelectItem>
                    ))}
                </SelectContent>
            </Select>
        </>
    );
};