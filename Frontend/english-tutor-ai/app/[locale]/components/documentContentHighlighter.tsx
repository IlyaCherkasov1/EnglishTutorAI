'use client'

import React, {useRef, useState} from 'react';
import {Button, Typography} from '@mui/material';
import SendIcon from "@mui/icons-material/Send";
import Box from '@mui/material/Box/Box';
import {Textarea} from "@/app/[locale]/components/textarea-autosize/textArea";
import {generateChatCompletion} from "@/app/api/textGeneration/textGenerationApi";
import DocumentCorrectionOutput from "@/app/[locale]/components/documentCorrectionOutput";
import {useI18n} from "@/app/locales/client";

interface Props {
    sentences: string[];
}

const DocumentContentHighlighter = (props: Props) => {
    const [currentLine, setCurrentLine] = useState(0);
    const [translatedText, setTranslatedText] = useState('');
    const [correctedText, setCorrectedText] = useState('');
    const [isCorrected, setIsCorrected] = useState(false);
    const t = useI18n()
    const formRef = useRef<HTMLFormElement>(null);

    const checkSentence = async (formData: FormData): Promise<void> => {
        const translatedText = formData.get('textarea-value') as string;

        const response = await generateChatCompletion({
            originalText: props.sentences[currentLine],
            translatedText: translatedText,
        })

        setTranslatedText(translatedText);
        setStates(response);
    }

    const setStates = (response: string) => {
        setCorrectedText(response);
        setCurrentLine((prevLine) => prevLine + 1)
        setIsCorrected(true);
    };

    return (
        <Box sx={{ maxWidth: '80%' }}>
            {props.sentences.map((line, index) => (
                <Typography
                    key={index}
                    component="span"
                    sx={{
                        color: index === currentLine ? 'black' : '#CCCCCC',
                        marginRight: '4px',
                    }}
                >
                    {line}
                </Typography>
            ))}
            <form ref={formRef} action={async (formData) => {
                await checkSentence(formData);
                formRef.current?.reset();
            }}>
                <Textarea sx={{ width: '100%' }}
                          name="textarea-value"
                          aria-label="minimum height"
                          minRows={1}
                          placeholder={t('enterYourText')}/>
                <Box sx={{ width: '100%', display: 'flex' }}>
                    <Button sx={{ marginLeft: 'auto' }} type="submit" variant="contained" endIcon={<SendIcon/>}>
                        {t('send')}
                    </Button>
                </Box>
            </form>
            {isCorrected ? (
                <DocumentCorrectionOutput correctedText={correctedText} translatedText={translatedText} />
            ) : (<> </>)
            }
        </Box>
    );
};

export default DocumentContentHighlighter;