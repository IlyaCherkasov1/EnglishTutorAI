import {StudyTopic} from "@/app/dataModels/enums/studyTopic.ts";

export const studyTopicColors: Record<keyof typeof StudyTopic, string> = {
    All: "bg-gray-100",
    Work: "bg-yellow-100",
    Travel: "bg-pink-100",
    Education: "bg-blue-100",
    Technology: "bg-indigo-100",
    Health: "bg-green-100",
    Environment: "bg-teal-100",
    Programming: "bg-cyan-100",
    Food: "bg-purple-100",
    Sports: "bg-orange-100",
    Shopping: "bg-red-100",
    Entertainment: "bg-lime-100",
    Other: "bg-gray-200",
};