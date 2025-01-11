import React from "react";

export const useEnterSubmit = (submitHandler: () => void) => {
    return (event: React.KeyboardEvent<HTMLTextAreaElement>) => {
        if (event.key === 'Enter' && !event.shiftKey) {
            event.preventDefault();
            submitHandler();
        }
    };
}