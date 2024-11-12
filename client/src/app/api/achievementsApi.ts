import {httpGet} from "@/app/infrastructure/requestApi.ts";
import {AchievementsResponse} from "@/app/dataModels/achievementsResponse.ts";

const achievementsResource = "achievements";

export const getAchievements = async (userId: string) : Promise<Array<AchievementsResponse>> => {
    return httpGet({ url: `${achievementsResource}/get-achievements/${userId}` });
}