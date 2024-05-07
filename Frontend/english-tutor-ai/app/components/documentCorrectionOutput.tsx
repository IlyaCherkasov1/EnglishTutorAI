import React from 'react';
import {Box, Button, Typography} from "@mui/material";
import SendIcon from "@mui/icons-material/Send";
import QuestionMarkIcon from '@mui/icons-material/QuestionMark';

interface Props {
    correctedText: string;
    translatedText: string;
}

const DocumentCorrectionOutput = (props: Props) => {
    return (
        <Box sx={{ backgroundColor: '#EAF5F5', marginTop: '10px' }}>
            <Typography sx={{ fontWeight: 'bold' }}>Исправленный текст:</Typography>
            <Typography sx={{ marginBottom: '10px'}}>{props.correctedText}</Typography>
            <Typography sx={{ fontWeight: 'bold' }}>Ваш перевод:</Typography>
            <Typography>{props.translatedText}</Typography>
            <Button sx={{ marginTop: '10px' }} variant="contained" endIcon={<QuestionMarkIcon/>}>
                ask AI
            </Button>
        </Box>
    );
};

export default DocumentCorrectionOutput;