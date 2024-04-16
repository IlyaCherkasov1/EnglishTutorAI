'use client'

import UserInput from "@/app/components/userInput";
import {useEffect, useState} from "react";
import {getStoryByIndex, getStoryCount} from "@/app/api/story/storyApi";
import StoryCarousel from "@/app/components/storyCarousel";

export default function Home() {
    const [correction, setCorrection] = useState('');
    const [storyContent, setStoryContent] = useState('');
    const [storyTitle, setStoryTitle] = useState('');
    const [currentStoryIndex, setCurrentStoryIndex] = useState(0);

    useEffect(() => {
        const fetchStoryByIndex = async () => {
            const response = await getStoryByIndex(currentStoryIndex);
            setStoryTitle(response.title);
            setStoryContent(response.content);
        }

        fetchStoryByIndex().catch(console.error);
    }, [currentStoryIndex])

    return (
        <>
            <main>
                <StoryCarousel
                    storyTitle={storyTitle}
                    storyContent={storyContent}
                    currentStoryIndex={currentStoryIndex}
                    setCurrentStoryIndex={setCurrentStoryIndex} />
                <UserInput setCorrection={setCorrection} storyContent={storyContent} />
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
