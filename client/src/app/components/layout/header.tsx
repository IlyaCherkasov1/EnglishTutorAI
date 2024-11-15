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
        <header className="bg-gray-100 border-b">
            <div className="grid grid-rows-[100px_1px_50px] px-7">
                <div className="flex items-center justify-between">
                    <Link to={routes.home} className="flex items-center">
                        <img src="/public/logo.svg" alt="Company Logo" className="h-10 w-10 mr-2" />
                        <span className="text-xl">{t("header.title")}</span>
                    </Link>
                    <div className="flex items-center">
                        <DropdownMenu>
                            <DropdownMenuTrigger asChild>
                                <HeaderButton variant="ghost" size="icon" className="rounded-full">
                                    <User className="h-5 w-5" />
                                    <span className="sr-only">{t("header.myAccount")}</span>
                                </HeaderButton>
                            </DropdownMenuTrigger>
                            <DropdownMenuContent align="end">
                                <DropdownMenuLabel>{contextStore.firstName}</DropdownMenuLabel>
                                <DropdownMenuSeparator />
                                <DropdownMenuItem>{t("header.profile")}</DropdownMenuItem>
                                <DropdownMenuItem onClick={performLogOut}>{t("header.signOut")}</DropdownMenuItem>
                            </DropdownMenuContent>
                        </DropdownMenu>
                    </div>
                </div>
                <Navbar />
            </div>
        </header>
    );
};