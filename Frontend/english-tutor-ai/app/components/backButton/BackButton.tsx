'use client'

import React from 'react';
import Button from '@mui/material/Button';
import ArrowBackIcon from '@mui/icons-material/ArrowBack';
import {useRouter} from "next/navigation";

const BackButton = () => {
    const router = useRouter();

    return (
        <Button
            startIcon={<ArrowBackIcon />}
            onClick={() => router.back()}
            variant="outlined"
        >
            Back
        </Button>
    );
};

export default BackButton;