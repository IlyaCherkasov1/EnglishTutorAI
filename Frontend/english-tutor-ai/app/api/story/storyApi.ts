import {StoryResponse} from "@/app/dataModels/story/storyResponse";

export const GetStory = async (): Promise<StoryResponse> => {
    const response = await fetch('https://localhost:7008/api/Story/get-story', {
        method: 'GET',
    })

   return  response.json();
}