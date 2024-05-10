'use client'

import React, {useState} from 'react';
import {Button, Typography} from '@mui/material';
import SendIcon from "@mui/icons-material/Send";
import Box from '@mui/material/Box/Box';
import {Textarea} from "@/app/components/textarea-autosize/textArea";
import {generateChatCompletion} from "@/app/api/textGeneration/textGenerationApi";
import DocumentCorrectionOutput from "@/app/components/documentCorrectionOutput";

interface Props {
    sentences: string[];
}

const DocumentContentHighlighter = (props: Props) => {
    const [currentLine, setCurrentLine] = useState(0);
    const [textAreaValue, setTextAreaValue] = useState('');
    const [translatedText, setTranslatedText] = useState('');
    const [correctedText, setCorrectedText] = useState('');
    const [isCorrected, setIsCorrected] = useState(false);

    const checkSentence = async (sentence: string): Promise<void> => {
        const response = await generateChatCompletion({
            originalText: sentence,
            translatedText: textAreaValue,
        })

        setStates(response);
    }

    const setStates = (response: string) => {
        setCorrectedText(response);
        setTranslatedText(textAreaValue);
        setTextAreaValue('');
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
            <Textarea sx={{ width: '100%' }}
                      aria-label="minimum height"
                      minRows={1}
                      onChange={(e) => setTextAreaValue(e.target.value)}
                      value={textAreaValue}
                      placeholder="Enter your text here..."/>
            <Box sx={{ width: '100%', display: 'flex' }}>
                <Button sx={{ marginLeft: 'auto' }}
                        onClick={() => checkSentence(props.sentences[currentLine])}
                        variant="contained" endIcon={<SendIcon/>}>
                    Send
                </Button>
            </Box>
            {isCorrected ? (
                <DocumentCorrectionOutput correctedText={correctedText} translatedText={translatedText}/>
            ) : (<> </>)
            }
        </Box>
    );
};

export default DocumentContentHighlighter;