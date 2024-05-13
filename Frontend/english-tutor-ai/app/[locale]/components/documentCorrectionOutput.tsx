import React from 'react';
import {Box, Button, Typography} from "@mui/material";
import QuestionMarkIcon from '@mui/icons-material/QuestionMark';
import {useI18n} from "@/app/locales/client";

interface Props {
    correctedText: string;
    translatedText: string;
}

const DocumentCorrectionOutput = (props: Props) => {
    const t = useI18n()

    return (
        <Box sx={{ backgroundColor: '#EAF5F5', marginTop: '10px' }}>
            <Typography sx={{ fontWeight: 'bold' }}>{t('correctedText')}:</Typography>
            <Typography sx={{ marginBottom: '10px'}}>{props.correctedText}</Typography>
            <Typography sx={{ fontWeight: 'bold' }}>{t('yourTranslation')}:</Typography>
            <Typography>{props.translatedText}</Typography>
            <Button sx={{ marginTop: '10px' }} variant="contained" endIcon={<QuestionMarkIcon/>}>
                {t('askAI')}
            </Button>
        </Box>
    );
};

export default DocumentCorrectionOutput;