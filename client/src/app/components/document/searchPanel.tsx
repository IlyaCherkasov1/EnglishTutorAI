import {Select, SelectContent, SelectItem, SelectTrigger, SelectValue} from "@/app/components/ui/select.tsx";
import {useTranslation} from "react-i18next";
import {getEnumValues} from "@/app/infrastructure/utils/enumUtils.ts";
import {StudyTopic} from "@/app/dataModels/enums/studyTopic.ts";
import {useLocation, useNavigate} from "react-router-dom";
import {objectToQueryString, queryStringToObject} from "@/app/infrastructure/utils/paramsUtils.ts";
import {useEffect, useState} from "react";
import {Button} from "@/app/components/ui/button.tsx";

const getStudyTopicFromUrl = () => {
    const params = queryStringToObject(window.location.search);
    return params["studyTopic"] || '';
};

export const SearchPanel = () => {
    const { t } = useTranslation();
    const navigate = useNavigate();
    const location = useLocation();
    const [selectedCategory, setSelectedCategory] = useState(getStudyTopicFromUrl);

    useEffect(() => {
        setSelectedCategory(getStudyTopicFromUrl());
    }, [location.search]);

    const handleCategoryChange = (category: string) => {
        const queryString = objectToQueryString({ studyTopic: category });
        setSelectedCategory(category);
        navigate(`?${queryString}`, { replace: true });
    };

    const handleResetFilters = () => {
        setSelectedCategory('');
        navigate('?');
    };

    return (
        <div
            className="flex items-center justify-between bg-white p-4 rounded-lg shadow-lg border border-gray-300 mt-5 mb-5">
            <div className="flex items-center space-x-3">
                <span className="text-gray-800 font-semibold">{t('category')}</span>
                <div className="relative">
                    <Select value={selectedCategory} onValueChange={handleCategoryChange}>
                        <SelectTrigger
                            className="w-48 bg-gray-50 border border-gray-300 rounded-md px-4 py-2 hover:shadow-sm transition-shadow">
                            <SelectValue placeholder={t('selectCategory')} />
                        </SelectTrigger>
                        <SelectContent>
                            {getEnumValues(StudyTopic).map((topic) => (
                                <SelectItem key={topic} value={topic}>
                                    {t(`studyTopics.${topic}`)}
                                </SelectItem>
                            ))}
                        </SelectContent>
                    </Select>
                </div>
            </div>
            <Button
                className="bg-gray-200 text-gray-700 border border-gray-300 px-4 py-2 rounded-md hover:bg-gray-300 transition-colors"
                onClick={handleResetFilters}>
                {t('clearAllFilters')}
            </Button>
        </div>
    )
};