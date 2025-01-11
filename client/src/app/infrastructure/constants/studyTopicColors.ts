import { StudyTopic } from "@/app/dataModels/enums/studyTopic.ts";

const baseColors = [
    "bg-yellow-100",
    "bg-pink-100",
    "bg-blue-100",
    "bg-indigo-100",
    "bg-green-100",
    "bg-teal-100",
    "bg-cyan-100",
    "bg-purple-100",
    "bg-orange-100",
    "bg-red-100",
    "bg-lime-100",
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