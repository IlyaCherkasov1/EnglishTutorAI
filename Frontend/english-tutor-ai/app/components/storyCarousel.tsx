'use client'

import {useEffect, useState} from "react";
import {GetStoryCount} from "@/app/api/story/storyApi";
import {SetGenericStateType} from "@/app/core/helpers/genericStateHelper";

interface Props{
    storyContent: string;
    currentStoryIndex: number;
    setCurrentStoryIndex: SetGenericStateType<number>;
}

export default function StoryCarousel(props: Props) {
    const [storyCount, setStoryCount] = useState<number>(0);

    useEffect(() => {
        const fetchStoryCount = async () => {
            const response = await GetStoryCount();
            setStoryCount(response);
        };

        fetchStoryCount().catch(console.error);
    }, []);

    const goToPreviousStory = () => {
        props.setCurrentStoryIndex((prevIndex) => Math.max(prevIndex - 1, 0));
    };

    const goToNextStory = () => {
        props.setCurrentStoryIndex((prevIndex) => Math.min(prevIndex + 1, storyCount - 1));
    };

    return (
        <div id="story-container">
            <div className="story-carousel">
                <div className="story">
                    <p id="story">{props.storyContent}</p>
                </div>
                <button onClick={ goToPreviousStory }>{ '<' }</button>
                <button onClick={ goToNextStory }>{ '>' }</button>
                <label>{ storyCount !== 0 ? `${ props.currentStoryIndex + 1 }/${ storyCount }` : 'Loading...' }</label>
            </div>
        </div>
    )
}