export interface UserAchievementResponse {
    achievementId: string;
    name: string;
    description: string;
    progress: number;
    goal: number;
    isCompleted: boolean;
    currentLevel: number;
    levelGoals: Array<number>;
    iconFileName: string;
}