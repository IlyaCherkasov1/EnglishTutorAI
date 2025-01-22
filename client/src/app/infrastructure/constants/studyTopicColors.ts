import { StudyTopic } from "@/app/dataModels/enums/studyTopic.ts";

const baseColors = [
    "bg-yellow-150",
    "bg-pink-150",
    "bg-blue-150",
    "bg-indigo-150",
    "bg-green-150",
    "bg-teal-150",
    "bg-cyan-150",
    "bg-purple-150",
    "bg-orange-150",
    "bg-red-150",
    "bg-lime-150",
];

const rotateArray = (arr: string[], times: number): string[] => {
    const length = arr.length;
    return arr.map((_, i) => arr[(i + times) % length]);
};

export const categoryColorSequences: Record<keyof typeof StudyTopic, string[]> = Object.keys(StudyTopic).reduce(
    (acc, topic, index) => {
        acc[topic as keyof typeof StudyTopic] = rotateArray(baseColors, index);
        return acc;
    },
    {} as Record<keyof typeof StudyTopic, string[]>
);