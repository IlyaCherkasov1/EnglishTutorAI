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
import {routes} from "@/app/components/layout/routes/routeLink.ts";
import {ScrollToTopOnRouteChange} from "@/app/components/scrollToTopOnRouteChange.ts";
import {UserProfile} from "@/app/components/profile/userProfile.tsx";

function App() {
    return (
        <BrowserRouter>
            <ScrollToTopOnRouteChange />
            <AuthGuard>
                <Routes>
                    <Route path={routes.login} element={<Login />} />
                    <Route path={routes.register} element={<Register />} />
                    <Route path={routes.home} element={<MainLayout />}>
                        <Route index element={<Home />} />
                        <Route path={routes.adminPanel}
                               element={<RequireRole role={userRoles.admin}><AdminPanel /> </RequireRole>} />
                        <Route path={routes.document.documentDetails} element={<DocumentDetails />} />
                        <Route path={routes.accessDenied} element={<AccessDenied />} />
                        <Route path={routes.history} element={<MistakeHistory />} />
                        <Route path={routes.profile} element={<UserProfile />} />
                    </Route>
                </Routes>
            </AuthGuard>
            <NotificationContainer />
        </BrowserRouter>
    )
}

export default App