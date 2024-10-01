import {BrowserRouter, Route, Routes} from "react-router-dom";
import DocumentDetails from "@/app/pages/documentDetails.tsx";
import Register from "@/app/pages/register.tsx";
import Login from "@/app/pages/login.tsx";
import AdminPanel from "@/app/pages/adminPanel.tsx";
import Home from "@/app/pages/home.tsx";
import {MainLayout} from "@/app/components/layout/mainLayout.tsx";
import {AuthGuard} from "@/app/components/component/auth/authGuard.tsx";

function App() {
    return (
        <BrowserRouter>
            <AuthGuard>
                    <Routes>
                        <Route path="/auth/login" element={<Login/>}/>
                        <Route path="/auth/register" element={<Register/>}/>
                        <Route path="/" element={<MainLayout/>}>
                            <Route index element={<Home/>}/>
                            <Route path="/adminPanel" element={<AdminPanel/>}/>
                            <Route path="/documents/:documentId" element={<DocumentDetails/>}/>
                        </Route>
                    </Routes>
                </AuthGuard>
        </BrowserRouter>
    )
}

export default App