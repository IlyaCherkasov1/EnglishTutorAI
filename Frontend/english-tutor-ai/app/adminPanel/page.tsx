'use client'

import {useState} from "react";
import {addStory} from "@/app/api/story/storyApi";

export default function AdminPanel() {
    const [title, setTitle] = useState ( '' );
    const [content, setContent] = useState ( '' );

    const handleOnClick = async() => {
        await addStory({title: title, content: content})
    }

    return (
        <>
            <p>Title:</p>
            <input id="story-title" onChange={(e) => setTitle(e.target.value)} />
            <p>Story Content:</p>
            <textarea id="story-input" onChange={( e ) => setContent(e.target.value)} />
            <button onClick={handleOnClick} id="submit-btn">Add</button>
        </>
    )
}