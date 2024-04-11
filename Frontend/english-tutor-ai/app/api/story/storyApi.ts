import {StoryResponse} from "@/app/dataModels/story/storyResponse";

export const GetStoryByIndex = async (index: number): Promise<StoryResponse> => {
    const response = await fetch(`https://localhost:7008/api/Story/get-story-by-index/${index}`, {
        method: 'GET',
    })

   return response.json();
}

export const GetStoryCount = async (): Promise<number> => {
    const response = await fetch("https://localhost:7008/api/Story/count", {
        method: 'GET'
    })

    return response.json();
}