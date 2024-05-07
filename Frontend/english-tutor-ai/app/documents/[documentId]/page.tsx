'use client'

import React, {useEffect, useState} from 'react';
import {getDocumentDetails, splitDocumentContent} from "@/app/api/document/documentApi";
import {DocumentResponse} from "@/app/dataModels/document/documentResponse";
import Typography from '@mui/material/Typography/Typography';
import Box from '@mui/material/Box/Box';
import Button from '@mui/material/Button';
import ArrowBackIcon from '@mui/icons-material/ArrowBack';
import Grid from '@mui/material/Grid/Grid';
import DocumentContentHighlighter from "@/app/components/documentContentHighlighter";

const DocumentDetails = ({ params }: { params: { documentId: string } }) => {
    const [document, setDocument] = useState<DocumentResponse>()
    const [sentences, setSentences] = useState<string[]>([]);

    useEffect(() => {
        getDocumentDetails(params.documentId)
            .then((data) => setDocument(data))
            .catch(console.error)
    }, []);


    useEffect(() => {
        if (document?.content) {
            splitDocumentContent({ text: document.content})
                .then((data) => setSentences(data))
                .catch(console.error);
        }
    }, [document]);

    const goBack = () => {
        window.history.back()
    };

    return document ? (
        <Box sx={{ display: 'flex', flexDirection: 'column', alignItems: 'center', height: '100vh', marginTop: '20px'}}>
            <Grid container sx={{ width: '80%', marginBottom: '20px', }} alignItems="center" justifyContent="space-between">
                <Grid item>
                    <Button
                        startIcon={<ArrowBackIcon />}
                        onClick={goBack}
                        variant="outlined">
                        Back
                    </Button>
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
                <p>Loading text...</p>
            )}
        </Box>
    ) : (
        <div>Loading..</div>
    )
};

export default DocumentDetails;