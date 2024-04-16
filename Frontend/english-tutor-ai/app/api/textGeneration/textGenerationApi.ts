import {RequestMethod} from "@/app/core/enum/requestMethod";

export const generateChatCompletion = async (text: string) => {
    const response = await fetch('https://localhost:7008/api/TextGeneration/generate-chat-completion', {
        method: RequestMethod.POST,
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ text: text })
    })

    return response.text();
}