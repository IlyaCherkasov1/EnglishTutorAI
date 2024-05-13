'use client'

import React from 'react';
import {Box, Link} from "@mui/material";
import List from "@mui/material/List";
import ListItem from "@mui/material/ListItem";
import ListItemText from "@mui/material/ListItemText";
import {DocumentListItem} from "@/app/dataModels/document/documentListItem";
import {DocumentListItemComponent} from "@/app/[locale]/components/document/documentListItemComponent";
import {useI18n} from "@/app/locales/client";

interface Props {
    allDocuments: DocumentListItem[];
}

export const DocumentsList = (props: Props) => {
    const t = useI18n()

    return (
        <main>
            <Box sx={{ backgroundColor: (theme) => theme.palette.background.default }}>
                <List>
                    {props.allDocuments ? (
                        props.allDocuments.map(document => (
                            <Link href={`/documents/${document.id}`} key={document.id} underline="none">
                                <DocumentListItemComponent document={document}/>
                            </Link>))
                    ) : (
                        <ListItem>
                            <ListItemText primary={t('loading')}/>
                        </ListItem>
                    )}
                </List>
            </Box>
        </main>
    );
};