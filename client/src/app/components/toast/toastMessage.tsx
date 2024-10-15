import "react-toastify/dist/ReactToastify.css";
import {getLocalizedMessage} from "@/app/infrastructure/utils/localizerUtils.ts";

interface Props {
    message?: string;
    messageKey?: string;
    traceId?: string | null;
}

export const ToastMessage = (props: Props) => {
    const message = props.message ?? getLocalizedMessage(props.messageKey!);
    const tracePart = props.traceId ? `. ${getLocalizedMessage("error")}: ${props.traceId}` : "";

    return (
        <>
            {message}
            {tracePart}
        </>
    );
};