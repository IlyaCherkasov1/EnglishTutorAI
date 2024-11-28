import {useState} from "react";
import {AchievementsResponse} from "@/app/dataModels/achievementsResponse.ts";
import useAsyncEffect from "use-async-effect";
import {getAchievements} from "@/app/api/achievementsApi.ts";
import {UserStatisticsResponse} from "@/app/dataModels/userStatisticsResponse.ts";
import {getUserStatistics} from "@/app/api/userStatisticsApi.ts";
import {UserStatistics} from "@/app/components/profile/userStatistics.tsx";
import {UserAchievement} from "@/app/components/profile/userAchievement.tsx";
import {UserMainInfo} from "@/app/components/profile/userMainInfo.tsx";

export const UserProfile = () => {
    const [achievements, setAchievements] = useState<Array<AchievementsResponse>>([]);
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
            <UserMainInfo />
            <UserStatistics correctedMistakes={statistics.correctedMistakes} />
            <UserAchievement achievements={achievements} />
        </div>
    );
};