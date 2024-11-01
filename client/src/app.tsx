import {BrowserRouter, Route, Routes} from "react-router-dom";
import {AuthGuard} from "@/app/components/auth/authGuard.tsx";
import Login from "@/app/components/auth/login.tsx";
import Register from "@/app/components/auth/register.tsx";
import {MainLayout} from "@/app/components/layout/mainLayout.tsx";
import {AdminPanel} from "@/app/components/pages/adminPanel.tsx";
import Home from "@/app/components/pages/home.tsx";
import DocumentDetails from "@/app/components/document/documentDetails.tsx";
import {NotificationContainer} from "@/app/components/toast/notificationContainer";
import {userRoles} from "@/app/infrastructure/constants/userRoles.ts";
import {RequireRole} from "@/app/components/auth/requireRole.tsx";
import {AccessDenied} from "@/app/components/pages/accessDenied.tsx";
import {MistakeHistory} from "@/app/components/pages/mistakeHistory.tsx";

function App() {
    return (
        <BrowserRouter>
            <AuthGuard>
                <Routes>
                    <Route path="/auth/login" element={<Login />} />
                    <Route path="/auth/register" element={<Register />} />
                    <Route path="/" element={<MainLayout />}>
                        <Route index element={<Home />} />
                        <Route path="/adminPanel"
                               element={<RequireRole role={userRoles.admin}><AdminPanel /> </RequireRole>} />
                        <Route path="/documents/:documentId" element={<DocumentDetails />} />
                        <Route path="/access-denied" element={<AccessDenied />} />
                        <Route path="/history" element={<MistakeHistory />} />
                    </Route>
                </Routes>
            </AuthGuard>
            <NotificationContainer />
        </BrowserRouter>
    )
}

export default App