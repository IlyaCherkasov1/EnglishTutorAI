import {httpGet} from "@/app/infrastructure/requestApi.ts";
import {UserAchievementResponse} from "@/app/dataModels/userAchievementResponse.ts";

const achievementsResource = "achievements";

export const getAchievements = async () : Promise<Array<UserAchievementResponse>> => {
    return httpGet({ url: `${achievementsResource}/get-achievements` });
}