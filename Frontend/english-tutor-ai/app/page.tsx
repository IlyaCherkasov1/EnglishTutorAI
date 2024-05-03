'use client'

import {useEffect, useState} from "react";
import List from '@mui/material/List';
import ListItem from '@mui/material/ListItem';
import ListItemAvatar from '@mui/material/ListItemAvatar';
import ListItemText from '@mui/material/ListItemText';
import Avatar from '@mui/material/Avatar';
import IconButton from '@mui/material/IconButton';
import FolderIcon from '@mui/icons-material/Folder';
import DeleteIcon from '@mui/icons-material/Delete';
import {getAllDocuments} from "@/app/api/document/documentApi";
import {DocumentListItem} from "@/app/dataModels/document/documentListItem";
import {formatDateToISO} from "@/app/core/helpers/dateHelpers";
import {Box} from "@mui/material";

export default function Home() {
    const [allDocuments, setAllDocuments] = useState<DocumentListItem[]>([]);

    useEffect(() => {
        const fetchDocuments = async () => {
            const response = await getAllDocuments();
            setAllDocuments(response);
        }

        fetchDocuments().catch(console.error);
    }, []);

    return (
        <>
            <main>
                <Box sx={{ backgroundColor: (theme) => theme.palette.background.default }}>
                    <List>
                        {allDocuments ? (
                            allDocuments.map(document => (
                                <ListItem
                                    key={document.title}
                                    secondaryAction={
                                        <IconButton edge="end" aria-label="delete">
                                            <DeleteIcon/>
                                        </IconButton>
                                    }>
                                    <ListItemAvatar>
                                        <Avatar>
                                            <FolderIcon/>
                                        </Avatar>
                                    </ListItemAvatar>
                                    <ListItemText
                                        primary={document.title}
                                        secondary={formatDateToISO(document.createdAt)}/>
                                </ListItem>))
                        ) : (
                            <ListItem>
                                <ListItemText primary="Loading..."/>
                            </ListItem>
                        )}
                    </List>
                </Box>
            </main>
        </>
    );
}