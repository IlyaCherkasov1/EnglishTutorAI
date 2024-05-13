'use client'

import {useState} from "react";
import {addDocument} from "@/app/api/document/documentApi";
import {useI18n} from "@/app/locales/client";

export default function AdminPanel() {
    const [title, setTitle] = useState ( '' );
    const [content, setContent] = useState ( '' );
    const t = useI18n()

    const handleOnClick = async() => {
        await addDocument({title: title, content: content})
    }

    return (
        <>
            <p>{t('title')}</p>
            <input id="document-title" onChange={(e) => setTitle(e.target.value)} />
            <p>{t('documentContent')}</p>
            <textarea id="document-input" onChange={( e ) => setContent(e.target.value)} />
            <button onClick={handleOnClick} id="submit-btn">{t('add')}</button>
        </>
    )
}