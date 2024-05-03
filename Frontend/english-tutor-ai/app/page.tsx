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
import {getAllStories} from "@/app/api/story/storyApi";
import {StoryListItem} from "@/app/dataModels/story/storyListItem";
import {formatDateToISO} from "@/app/core/helpers/dateHelpers";
import {Box} from "@mui/material";

export default function Home() {
    const [allStories, setAllStories] = useState<StoryListItem[]>([]);

    useEffect(() => {
        const fetchStories = async () => {
            const response = await getAllStories();
            setAllStories(response);
        }

        fetchStories().catch(console.error);
    }, []);

    return (
        <>
            <main>
                <Box sx={{ backgroundColor: (theme) => theme.palette.background.default }}>
                    <List>
                        {allStories ? (
                            allStories.map(story => (
                                <ListItem
                                    key={story.title}
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
                                        primary={story.title}
                                        secondary={formatDateToISO(story.createdAt)}/>
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