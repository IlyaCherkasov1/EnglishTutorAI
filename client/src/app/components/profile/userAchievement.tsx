import {useTranslation} from "react-i18next";
import {AchievementsResponse} from "@/app/dataModels/achievementsResponse.ts";

interface Props {
    achievements: Array<AchievementsResponse>;
}

export const UserAchievement = (props: Props) => {
    const { t } = useTranslation();

    return (
        <div>
            <h1 className="text-lg font-semibold">{t('achievementsLabel')}</h1>
            <ul className="divide-y divide-gray-200">
                {props.achievements.map((achievement) => {
                    const progressPercentage = Math.min(
                        (achievement.progress / achievement.levelGoals[achievement.currentLevel]) * 100,
                        100
                    );

                    return (
                        <li className="py-4 flex items-center" key={achievement.id}>
                            <div className="relative flex-shrink-0 flex flex-col items-center w-24">
                                <div
                                    className="w-20 h-20 flex items-center justify-center rounded-full
                                        bg-gradient-to-r from-purple-400 to-blue-400 shadow-lg relative">
                                    <img
                                        src={`/achievementsIcons/${achievement.iconFileName}`}
                                        alt={t(achievement.name)}
                                        className="w-12 h-12 relative"
                                    />
                                    <div
                                        className="absolute -bottom-3 left-1/2 transform -translate-x-1/2 bg-white
                                            rounded-full px-2 py-1">
                                            <span className="text-xs font-bold text-indigo-600">
                                            {achievement.isCompleted ? t('completedLabel') : `LV.${achievement.currentLevel + 1}`}
                                            </span>
                                    </div>
                                </div>
                            </div>

                            <div className="ml-4 flex-1">
                                <h3 className="text-lg font-bold text-gray-800">{t(achievement.name)}</h3>
                                <p className="text-sm text-gray-600">{t(achievement.description)}</p>
                                {!achievement.isCompleted &&
                                    (<div className="flex items-center mt-3">
                                        <div className="flex-1 bg-gray-300 rounded-full h-2 relative">
                                            <div className="absolute top-0 left-0 h-2 rounded-full transition-all"
                                                 style={{
                                                     width: `${progressPercentage}%`,
                                                     backgroundColor: '#F87171',
                                                 }}>
                                            </div>
                                        </div>
                                        <div className="text-sm text-gray-500 ml-3 font-medium">
                                            {achievement.progress} / {achievement.levelGoals[achievement.currentLevel]}
                                        </div>
                                    </div>)}
                            </div>
                        </li>
                    );
                })}
            </ul>
        </div>
    );
}