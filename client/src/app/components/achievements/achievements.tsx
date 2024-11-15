import { useState } from "react";
import { AchievementsResponse } from "@/app/dataModels/achievementsResponse.ts";
import useAsyncEffect from "use-async-effect";
import { getAchievements } from "@/app/api/achievementsApi.ts";
import { useTranslation } from "react-i18next";
import {UserStatisticsResponse} from "@/app/dataModels/userStatisticsResponse.ts";
import {getUserStatistics} from "@/app/api/userStatisticsApi.ts";

export const Achievements = () => {
    const { t } = useTranslation();
    const [achievements, setAchievements] = useState<Array<AchievementsResponse>>([]);
    const [statistics, setStatistics] = useState<UserStatisticsResponse>();

    useAsyncEffect(async () => {
        const [achievementsData, statisticsData] = await Promise.all([
            getAchievements(),
            getUserStatistics()
        ]);
        setAchievements(achievementsData);
        setStatistics(statisticsData);
    }, []);

    return (
        <div className="bg-gray-100 p-4 rounded-lg">
            {statistics && (
                <div className="mb-8 text-center bg-white shadow-sm rounded-lg p-5">
                    <h2 className="text-xl font-semibold text-gray-800">{t('correctedMistakes')}</h2>
                    <p className="text-3xl font-bold text-green-600 mt-2">
                        {statistics.correctedMistakes}
                    </p>
                </div>
            )}
            <ul>
                {achievements.map(achievement => {
                    const progressPercentage = Math.min(
                        (achievement.progress / achievement.levelGoals[achievement.currentLevel]) * 100,
                        100
                    );

                    return (
                        <li className="mb-6 bg-white shadow-lg rounded-lg p-4 flex items-start space-x-4" key={achievement.id}>
                            <div className="w-12 h-12 flex items-center justify-center bg-gray-200 rounded-full">
                                <img
                                    src={`/achievementsIcons/${achievement.iconFileName}`}
                                    alt={t(achievement.name)}
                                    className="w-8 h-8"
                                />
                            </div>
                            <div className="flex-1">
                                <h3 className="text-lg font-semibold text-gray-800">{t(achievement.name)}</h3>
                                <p className="text-gray-500 text-sm mb-1">{t(achievement.description)}</p>
                                <p className="text-sm text-gray-500">{t('level')}: {achievement.currentLevel + 1}</p>

                                <div className="w-full bg-gray-200 rounded-full h-2.5 mt-2">
                                    <div
                                        className="h-2.5 rounded-full"
                                        style={{
                                            width: `${progressPercentage}%`,
                                            backgroundColor: achievement.isCompleted ? '#4CAF50' : '#F87171',
                                        }}
                                    ></div>
                                </div>

                                <p className="text-sm text-gray-400 mt-1">
                                    {t('progress')}: {achievement.progress} / {achievement.levelGoals[achievement.currentLevel]}
                                </p>
                            </div>
                        </li>
                    );
                })}
            </ul>
        </div>
    );
};