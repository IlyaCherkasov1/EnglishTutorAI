import {StoryResponse} from "@/app/dataModels/story/storyResponse";
import {RequestMethod} from "@/app/core/enum/requestMethod";
import {StoryCreationRequest} from "@/app/dataModels/story/StoryCreationRequest";
import {StoryListItem} from "@/app/dataModels/story/storyListItem";

export const getStoryByIndex = async ( index: number): Promise<StoryResponse> => {
    const response = await fetch(`https://localhost:7008/api/Story/get-story-by-index/${index}`, {
        method: RequestMethod.GET,
    })

   return response.json();
}

export const getStoryCount = async (): Promise<number> => {
    const response = await fetch("https://localhost:7008/api/Story/count", {
        method: RequestMethod.GET
    })

    return response.json();
}

export const addStory = async (request: StoryCreationRequest ) => {
    await fetch("https://localhost:7008/api/Story/add-story", {
        method: RequestMethod.POST,
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(request),
    })
}

export const getAllStories = async (): Promise<StoryListItem[]> => {
    const response = await fetch(`https://localhost:7008/api/Story/get-stories`, {
        method: RequestMethod.GET,
    })

    return response.json();
}