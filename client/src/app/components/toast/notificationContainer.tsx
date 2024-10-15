import {ToastContainer} from "react-toastify";

export const NotificationContainer = () =>
    <ToastContainer
        position='top-center'
        autoClose={3000}
        draggable
        pauseOnHover
        closeOnClick
        icon={false}
    />;