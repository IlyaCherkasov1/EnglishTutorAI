'use client'

import UserInput from "@/app/components/userInput";
import {useState} from "react";
import {GetStory} from "@/app/api/story/storyApi";

export default function Home() {
    const [correction, setCorrection] = useState('');
    const [storyContent, setStoryContent] = useState('');

    const generateStory = async ()=> {
        const response = await GetStory();
        setStoryContent(response.content);
    }

    return (
        <>
            <main>
                <div id="story-container">
                    <p id="story">{storyContent}</p>
                    <button onClick={generateStory}>Generate story</button>
                </div>
                <UserInput setCorrection={setCorrection}/>
                <div id="correction">
                    <p id="correction-text">{correction}</p>
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
