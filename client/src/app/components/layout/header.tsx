import {User} from "lucide-react";
import {HeaderButton} from "@/app/components/ui/headerButton";
import {
    DropdownMenu,
    DropdownMenuContent,
    DropdownMenuItem,
    DropdownMenuLabel,
    DropdownMenuSeparator,
    DropdownMenuTrigger,
} from "@/app/components/ui/dropdown-menu";
import {performLogOut} from "@/app/infrastructure/services/auth/identityService";
import {useTranslation} from "react-i18next";
import {contextStore} from "@/app/infrastructure/stores/contextStore";
import {Link} from "react-router-dom";
import {Navbar} from "@/app/components/layout/navbar.tsx";
import {routes} from "@/app/components/layout/routes/routeLink.ts";

export const Header = () => {
    const { t } = useTranslation();

    return (
        <header className="border-b h-16 bg-softBeige fixed top-0 z-50 w-full">
            <div className="flex items-center justify-between max-w-5xl mx-auto mt-3 space-x-4 px-4">
                <Link to={routes.home} className="flex space-x-4">
                    <img src="/public/logo.png" alt="Company Logo" className="h-8 w-8 " />
                    <p className="font-serif text-2xl font-bold">{t("header.title")}</p>
                </Link>
                <div className="flex justify-center space-x-4">
                    <Navbar />
                </div>
                <div className="flex space-x-4">
                    <DropdownMenu>
                        <DropdownMenuTrigger asChild>
                            <HeaderButton variant="ghost" size="icon" className="rounded-full">
                                <User className="h-5 w-5" />
                                <span className="sr-only">{t("header.myAccount")}</span>
                            </HeaderButton>
                        </DropdownMenuTrigger>
                        <DropdownMenuContent align="end">
                            <DropdownMenuLabel>{contextStore.firstName}</DropdownMenuLabel>
                            <div className="px-2 py-1.5 text-sm text-gray-600">
                                {contextStore.email}
                            </div>
                            <DropdownMenuSeparator />
                            <DropdownMenuItem onClick={performLogOut}>{t("header.signOut")}</DropdownMenuItem>
                        </DropdownMenuContent>
                    </DropdownMenu>
                </div>
            </div>
        </header>
    );
};