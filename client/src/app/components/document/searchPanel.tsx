import {Select, SelectContent, SelectItem, SelectTrigger, SelectValue} from "@/app/components/ui/select.tsx";
import {useTranslation} from "react-i18next";
import {getEnumValues} from "@/app/infrastructure/utils/enumUtils.ts";
import {StudyTopic} from "@/app/dataModels/enums/studyTopic.ts";
import {Button} from "@/app/components/ui/button.tsx";

interface Props {
    selectedCategory: string;
    onCategoryChange: (category: string) => void;
}

export const SearchPanel = (props: Props) => {
    const { t } = useTranslation();

    const handleCategoryChange = (category: string) => {
        props.onCategoryChange(category);
    };

    const handleResetFilters = () => {
        props.onCategoryChange(StudyTopic[StudyTopic.All]);
    };

    return (
        <div
            className="flex items-center justify-between bg-white p-4 rounded-lg shadow-lg border border-gray-300 mt-5 mb-5">
            <div className="flex items-center space-x-3">
                <span className="text-gray-800 font-semibold">{t('category')}</span>
                <div className="relative">
                    <Select value={props.selectedCategory} onValueChange={handleCategoryChange}>
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