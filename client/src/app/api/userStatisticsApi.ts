import {UserStatisticsResponse} from "@/app/dataModels/userStatisticsResponse.ts";
import {httpGet} from "@/app/infrastructure/requestApi.ts";

const UserStatisticsResource = "userStatistics";

export const getUserStatistics = async () : Promise<UserStatisticsResponse> => {
    return httpGet({url: `${UserStatisticsResource}/get-statistics`});
}