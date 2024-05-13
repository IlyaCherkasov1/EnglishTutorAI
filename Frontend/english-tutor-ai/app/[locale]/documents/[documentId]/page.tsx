import React from 'react';
import {getDocumentDetails, splitDocumentContent} from "@/app/api/document/documentApi";
import Typography from '@mui/material/Typography/Typography';
import Box from '@mui/material/Box/Box';
import Grid from '@mui/material/Grid/Grid';
import DocumentContentHighlighter from "@/app/[locale]/components/documentContentHighlighter";
import BackButton from '@/app/[locale]/components/buttons/backButton/backButton';
import {getI18n} from "@/app/locales/server";

const DocumentDetails = async ({ params }: { params: { documentId: string } }) => {
    const document = await getDocumentDetails(params.documentId);
    const sentences = await splitDocumentContent({ text: document.content });
    const t = await getI18n()

    return document ? (
        <Box sx={{ display: 'flex', flexDirection: 'column', alignItems: 'center', height: '100vh', marginTop: '20px'}}>
            <Grid container sx={{ width: '80%', marginBottom: '20px', }} alignItems="center" justifyContent="space-between">
                <Grid item>
                    <BackButton />
                </Grid>
                <Grid item xs>
                    <Typography sx={{ textAlign: 'center', }} variant="h5" gutterBottom>
                        {document.title}
                    </Typography>
                </Grid>
            </Grid>
            {sentences.length > 0 ? (
                <DocumentContentHighlighter sentences={sentences} />
            ) : (
                <p>{t('loading')}</p>
            )}
        </Box>
    ) : (
        <div>{t('loading')}</div>
    )
};

export default DocumentDetails;