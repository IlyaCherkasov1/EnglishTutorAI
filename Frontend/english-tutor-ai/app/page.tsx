'use client'

import UserInput from "@/app/components/userInput";
import {useEffect, useState} from "react";
import {GetStoryByIndex, GetStoryCount} from "@/app/api/story/storyApi";
import StoryCarousel from "@/app/components/storyCarousel";

export default function Home() {
    const [correction, setCorrection] = useState('');
    const [storyContent, setStoryContent] = useState('');
    const [currentStoryIndex, setCurrentStoryIndex] = useState(0);

    useEffect(() => {
        const fetchStoryByIndex = async () => {
            const response = await GetStoryByIndex(currentStoryIndex);
            setStoryContent(response.content);
        }

        fetchStoryByIndex().catch(console.error);
    }, [currentStoryIndex])

    return (
        <>
            <main>
                <StoryCarousel
                    storyContent={storyContent}
                    currentStoryIndex={currentStoryIndex}
                    setCurrentStoryIndex={setCurrentStoryIndex} />
                <UserInput setCorrection={setCorrection}/>
                <div id="correction">
                    <p id="correction-text">{ correction }</p>
                    <p id="correction-feedback">Feedback</p>
                </div>
                <div id="translation">
                    <p id="translation-text">AIs Translation:</p>
                </div>
            </main>
            <footer>
                <p>English Learning Assistant 2024</p>
            </footer>
        </>
    );
}
