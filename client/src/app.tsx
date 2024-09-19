import {Route, Routes, useLocation} from "react-router-dom";
import DocumentDetails from "./app/pages/documentDetails.tsx";
import Register from "./app/pages/register.tsx";
import Login from "./app/pages/login.tsx";
import AdminPanel from "./app/pages/adminPanel.tsx";
import Home from "./app/pages/home.tsx";
import {Header} from "./app/components/component/header.tsx";

function App() {
    const location = useLocation();

    const shouldShowHeader = !(
        location.pathname === "/auth/login" || location.pathname === "/auth/register"
    );

    return (
        <>
            {shouldShowHeader && <Header/>}
            <Routes>
                <Route path="/" element={<Home/>}/>
                <Route path="/adminPanel" element={<AdminPanel/>}/>
                <Route path="/auth/login" element={<Login/>}/>
                <Route path="/auth/register" element={<Register/>}/>
                <Route path="/documents/:documentId" element={<DocumentDetails/>}/>
            </Routes>
        </>
    )
}

export default App