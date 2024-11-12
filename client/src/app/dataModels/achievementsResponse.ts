export interface AchievementsResponse {
    id: string;
    name: string;
    description: string;
    progress: number;
    goal: number;
    isCompleted: boolean;
    currentLevel: number;
    levelGoals: Array<number>;
    iconFileName: string;
}