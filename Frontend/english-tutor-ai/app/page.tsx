'use client'

import UserInput from "@/app/components/userInput";
import {useEffect, useState} from "react";
import {GetStoryByIndex, GetStoryCount} from "@/app/api/story/storyApi";

export default function Home() {
    const [correction, setCorrection] = useState('');
    const [storyContent, setStoryContent] = useState('');
    const [storyCount, setStoryCount] = useState<number>(0);
    const [currentStoryIndex, setCurrentStoryIndex] = useState(0);

    useEffect(() => {
        const fetchStoryCount = async () => {
            const response = await GetStoryCount();
            setStoryCount(response);
        };

        fetchStoryCount().catch(console.error);
    }, []);

    useEffect(() => {
        const fetchStoryByIndex = async () => {
            const response = await GetStoryByIndex(currentStoryIndex);
            setStoryContent(response.content);
        }

        fetchStoryByIndex().then(console.error);
    }, [currentStoryIndex])

    const goToPreviousStory = () => {
        setCurrentStoryIndex((prevIndex) => Math.max(prevIndex - 1, 0));
    };

    const goToNextStory = () => {
        setCurrentStoryIndex((prevIndex) => Math.min(prevIndex + 1, storyCount - 1));
    };

    return (
        <>
            <main>
                <div id="story-container">
                    <div className="story-carousel">
                        <div className="story">
                            <p id="story">{ storyContent }</p>
                        </div>
                        <button onClick={ goToPreviousStory }>{ '<' }</button>
                        <button onClick={ goToNextStory }>{ '>' }</button>
                        <label>{ storyCount !== 0 ? `${ currentStoryIndex + 1 }/${ storyCount }` : 'Loading...' }</label>
                    </div>
                </div>
                <UserInput setCorrection={ setCorrection }/>
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
