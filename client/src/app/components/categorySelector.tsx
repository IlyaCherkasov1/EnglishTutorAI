import {useState, useRef, useEffect} from "react";
import {getEnumValues} from "@/app/infrastructure/utils/enumUtils.ts";
import {StudyTopic} from "@/app/dataModels/enums/studyTopic.ts";
import {ChevronLeft, ChevronRight} from "lucide-react";

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

    const categories = getEnumValues(StudyTopic);

    const updateScrollButtons = () => {
        if (scrollContainerRef.current) {
            const { scrollLeft, scrollWidth, clientWidth } = scrollContainerRef.current;
            setCanScrollLeft(scrollLeft > 0);
            setCanScrollRight(scrollLeft + clientWidth < scrollWidth);
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
        <div className="flex items-center bg-white px-4 py-2 rounded-lg">
            {canScrollLeft && (
                <button
                    className="p-2 rounded-full bg-gray-100 hover:bg-gray-200 shadow-whiteGlow"
                    onClick={() => handleScroll("left")}>
                    <ChevronLeft size={20} className="text-gray-700" />
                </button>
            )}

            <div className="flex gap-2 overflow-x-auto scrollbar-hide"
                 ref={scrollContainerRef}>
                {categories.map((category) => (
                    <button
                        key={category}
                        className={`px-4 py-2 text-sm font-medium rounded-full whitespace-nowrap ${
                            activeCategory === category
                                ? "bg-black text-white"
                                : "bg-gray-100 text-gray-700 hover:bg-gray-200"
                        }`}
                        onClick={() => handleCategoryClick(category)}>
                        {category}
                    </button>
                ))}
            </div>

            {canScrollRight && (
                <button
                    className="p-2 rounded-full bg-gray-100 hover:bg-gray-200 shadow-whiteGlow"
                    onClick={() => handleScroll("right")}>
                    <ChevronRight size={20} className="text-gray-700" />
                </button>
            )}
        </div>
    );
};