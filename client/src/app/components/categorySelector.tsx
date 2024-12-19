import {useState, useRef, useEffect} from "react";
import {getEnumValues} from "@/app/infrastructure/utils/enumUtils.ts";
import {StudyTopic} from "@/app/dataModels/enums/studyTopic.ts";
import {ChevronLeft, ChevronRight} from "lucide-react";
import {useTranslation} from "react-i18next";

interface Props {
    selectedCategory: string;
    onCategoryChange: (category: string) => void;
}

export const CategorySelector = ({ selectedCategory, onCategoryChange }: Props) => {
    const [activeCategory, setActiveCategory] = useState(
        selectedCategory || StudyTopic[StudyTopic.All]
    );
    const scrollContainerRef = useRef<HTMLDivElement>(null);
    const [canScrollLeft, setCanScrollLeft] = useState(false);
    const [canScrollRight, setCanScrollRight] = useState(true);
    const { t } = useTranslation();

    const categories = getEnumValues(StudyTopic);

    const updateScrollButtons = () => {
        if (scrollContainerRef.current) {
            const { scrollLeft, scrollWidth, clientWidth } = scrollContainerRef.current;
            setCanScrollLeft(scrollLeft > 0);
            const ERROR_MARGIN = 5;
            setCanScrollRight(scrollLeft + clientWidth < scrollWidth - ERROR_MARGIN);
        }
    };

    const handleScroll = (direction: "left" | "right") => {
        if (scrollContainerRef.current) {
            const clientWidth = scrollContainerRef.current.clientWidth;
            const scrollStep = clientWidth * 0.8;

            scrollContainerRef.current.scrollBy({
                left: direction === "left" ? -scrollStep : scrollStep,
                behavior: "smooth",
            });
        }
    };

    const handleCategoryClick = (category: string) => {
        if (activeCategory === category) {
            return;
        }

        setActiveCategory(category);
        onCategoryChange(category);
    };

    useEffect(() => {
        updateScrollButtons();
        const scrollContainer = scrollContainerRef.current;
        if (scrollContainer) {
            scrollContainer.addEventListener("scroll", updateScrollButtons);
            return () => scrollContainer.removeEventListener("scroll", updateScrollButtons);
        }
    }, []);

    return (
        <div className="flex items-center px-4 py-4 rounded-lg">
            {canScrollLeft && (
                <button
                    className="p-3 rounded-full bg-gray-100 hover:bg-gray-200 shadow-softGlow z-10"
                    onClick={() => handleScroll("left")}>
                    <ChevronLeft size={20} className="text-gray-700" />
                </button>
            )}

            <div className="flex gap-4 overflow-x-auto scrollbar-hide items-center" ref={scrollContainerRef}>
                {categories.map((category) => (
                    <button
                        key={category}
                        className={`flex items-center gap-3 px-6 py-3 rounded-full font-medium shadow-sm whitespace-nowrap transition-all ${
                            activeCategory === category
                                ? "bg-black text-white"
                                : "bg-gray-100 text-gray-700 hover:bg-gray-200"
                        }`}
                        onClick={() => handleCategoryClick(category)}>
                        <span className="flex items-center justify-center w-10 h-10 rounded-full bg-white shadow">
                            <img src={`/public/topics/${category.toLowerCase()}.png`}
                                 alt={`${category} icon`}
                                 className="w-6 h-6 object-contain" />
                        </span>
                        <span>{t(`studyTopics.${category}`)}</span>
                    </button>
                ))}
            </div>

            {canScrollRight && (
                <button
                    className="p-3 rounded-full bg-gray-100 hover:bg-gray-200 shadow-softGlow z-10"
                    onClick={() => handleScroll("right")}>
                    <ChevronRight size={20} className="text-gray-700" />
                </button>
            )}
        </div>
    );
};