import {toast} from 'react-toastify';
import "react-toastify/dist/ReactToastify.css";
import {ToastMessage} from "@/app/components/toast/toastMessage.tsx";
import '@/app/components/toast/toast.scss';

export const notifications = {
    error(message: string) {
        toast.error(<ToastMessage message={message} />);
    },
    success(message: string) {
        toast.success(<ToastMessage message={message} />);
    },
    defaultError(traceId?: string | null) {
        toast.error(<ToastMessage messageKey="defaultRequestError" traceId={traceId} />);
    },
};