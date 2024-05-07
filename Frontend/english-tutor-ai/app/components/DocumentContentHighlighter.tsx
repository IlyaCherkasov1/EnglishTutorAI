import React, {useState} from 'react';
import {Button, Typography} from '@mui/material';
import SendIcon from "@mui/icons-material/Send";
import Box from '@mui/material/Box/Box';
import {Textarea} from "@/app/components/textarea-autosize/textArea";

interface Props {
    sentences: string[];
}

const DocumentContentHighlighter = (props: Props) => {
    const [currentLine, setCurrentLine] = useState(0);

    const highlightNext = () => {
        setCurrentLine((prevLine) => prevLine + 1);
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
            <Textarea sx={{ width: '100%' }} aria-label="minimum height" minRows={1}
                      placeholder="Enter your text here..."/>
            <Box sx={{ width: '100%', display: 'flex' }}>
                <Button sx={{ marginLeft: 'auto' }} onClick={highlightNext} variant="contained" endIcon={<SendIcon/>}>
                    Send
                </Button>
            </Box>
        </Box>
    );
};

export default DocumentContentHighlighter;