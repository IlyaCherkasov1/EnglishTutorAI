import {useState} from 'react';
import {getEnumValues} from "@/app/infrastructure/utils/enumUtils.ts";
import {StudyTopic} from "@/app/dataModels/enums/studyTopic.ts";

interface Props {
    selectedCategory: string;
    onCategoryChange: (category: string) => void;
}

export const CategorySelector = (props: Props) => {
    const [activeCategory, setActiveCategory] = useState(StudyTopic[StudyTopic.All]);

    const handleCategoryClick = (category: string) => {
        setActiveCategory(category);
        props.onCategoryChange(category);
    };

    return (
        <div className="flex gap-4 overflow-x-auto py-4 px-2 bg-white">
            {getEnumValues(StudyTopic).map((studyTopic) => (
                <button
                    key={studyTopic}
                    className={`px-4 py-2 rounded-full text-sm font-medium whitespace-nowrap ${
                        activeCategory === studyTopic
                            ? 'bg-black text-white'
                            : 'bg-gray-100 text-gray-700 hover:bg-gray-200'
                    }`}
                    onClick={() => handleCategoryClick(studyTopic)}>
                    {studyTopic}
                </button>
            ))}
        </div>
    );
};