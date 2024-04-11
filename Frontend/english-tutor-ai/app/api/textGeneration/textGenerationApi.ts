export const GenerateChatCompletion = async (text: string) => {
    const response = await fetch('https://localhost:7008/api/TextGeneration/generate-chat-completion', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify({ text: text })
    })

    return response.text();
}