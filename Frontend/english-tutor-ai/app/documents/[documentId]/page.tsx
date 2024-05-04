'use client'

import React, {useEffect, useState} from 'react';
import {getDocumentDetails} from "@/app/api/document/documentApi";
import {DocumentResponse} from "@/app/dataModels/document/documentResponse";
import Typography from '@mui/material/Typography/Typography';
import Box from '@mui/material/Box/Box';
import {Textarea} from "@/app/components/textarea-autosize/textArea";
import Button from '@mui/material/Button';
import SendIcon from '@mui/icons-material/Send';
import ArrowBackIcon from '@mui/icons-material/ArrowBack';
import Grid from '@mui/material/Grid/Grid';

const DocumentDetails = ({ params }: { params: { documentId: string } }) => {
    const [document, setDocument] = useState<DocumentResponse>()

    useEffect(() => {
        const fetchDocument = async () => {
            const response = await getDocumentDetails(params.documentId);
            setDocument(response);
        }

        fetchDocument().catch(console.error);
    }, []);

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
            <Typography sx={{ maxWidth: '80%' }} variant="body2" gutterBottom>
                {document.content}
            </Typography>
            <Textarea sx={{ width: '80%' }} aria-label="minimum height" minRows={1} placeholder="Enter your text here..."/>
            <Box sx={{ width: '80%', display: 'flex' }}>
                <Button sx={{ marginLeft: 'auto' }} variant="contained" endIcon={<SendIcon/>}>
                    Send
                </Button>
            </Box>
        </Box>
    ) : (
        <div>Loading..</div>
    )
};

export default DocumentDetails;