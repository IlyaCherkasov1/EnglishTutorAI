import {useState} from "react";
import {UserAchievementResponse} from "@/app/dataModels/userAchievementResponse.ts";
import useAsyncEffect from "use-async-effect";
import {getAchievements} from "@/app/api/achievementsApi.ts";
import {UserStatisticsResponse} from "@/app/dataModels/userStatisticsResponse.ts";
import {getUserStatistics} from "@/app/api/userStatisticsApi.ts";
import {UserStatistics} from "@/app/components/profile/userStatistics.tsx";
import {UserAchievement} from "@/app/components/profile/userAchievement.tsx";
import {UserMainInfo} from "@/app/components/profile/userMainInfo.tsx";
import {LanguageSwitcher} from "@/app/components/profile/languageSwitcher.tsx";

export const UserProfile = () => {
    const [achievements, setAchievements] = useState<Array<UserAchievementResponse>>([]);
    const [statistics, setStatistics] = useState<UserStatisticsResponse>();

    useAsyncEffect(async () => {
        const [achievementsData, statisticsData] = await Promise.all([
            getAchievements(),
            getUserStatistics()
        ])
        setAchievements(achievementsData);
        setStatistics(statisticsData);
    }, []);

    if (!statistics) {
        return null;
    }

    return (
        <div className="p-4 min-h-screen rounded-lg">
            <div className="mb-8">
                <UserMainInfo />
            </div>
            <div className="mb-20">
                <LanguageSwitcher />
            </div>
            <UserStatistics correctedMistakes={statistics.correctedMistakes} />
            <UserAchievement achievements={achievements} />
        </div>
    );
};