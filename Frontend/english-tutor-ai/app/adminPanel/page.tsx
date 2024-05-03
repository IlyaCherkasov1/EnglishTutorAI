'use client'

import {useState} from "react";
import {addDocument} from "@/app/api/document/documentApi";

export default function AdminPanel() {
    const [title, setTitle] = useState ( '' );
    const [content, setContent] = useState ( '' );

    const handleOnClick = async() => {
        await addDocument({title: title, content: content})
    }

    return (
        <>
            <p>Title:</p>
            <input id="document-title" onChange={(e) => setTitle(e.target.value)} />
            <p>Document Content:</p>
            <textarea id="document-input" onChange={( e ) => setContent(e.target.value)} />
            <button onClick={handleOnClick} id="submit-btn">Add</button>
        </>
    )
}