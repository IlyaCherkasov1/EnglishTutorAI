import React from 'react';
import {DocumentListItem} from "@/app/dataModels/document/documentListItem";
import IconButton from "@mui/material/IconButton";
import DeleteIcon from "@mui/icons-material/Delete";
import ListItemAvatar from "@mui/material/ListItemAvatar";
import Avatar from "@mui/material/Avatar";
import FolderIcon from "@mui/icons-material/Folder";
import ListItemText from "@mui/material/ListItemText";
import {formatDateToISO} from "@/app/core/helpers/dateHelpers";
import ListItem from "@mui/material/ListItem";

interface Props {
    document: DocumentListItem;
}

export const DocumentListItemComponent = (props: Props) => {
    return (
        <>
            <ListItem
                key={props.document.id}
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
                    primary={props.document.title}
                    secondary={formatDateToISO(props.document.createdAt)}/>
            </ListItem>

        </>
    );
};