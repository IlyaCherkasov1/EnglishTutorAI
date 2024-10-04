import { Menu, User } from "lucide-react"
import { Link } from "react-router-dom";

import { HeaderButton } from "@/app/components/ui/headerButton.tsx"
import {
  DropdownMenu,
  DropdownMenuContent,
  DropdownMenuItem,
  DropdownMenuLabel,
  DropdownMenuSeparator,
  DropdownMenuTrigger,
} from "@/app/components/ui/dropdown-menu"
import {performLogOut} from "@/app/infrastructure/services/auth/identityService.ts";
import {useTranslation} from "react-i18next";
import {contextStore} from "@/app/infrastructure/stores/contextStore.ts";

export const Header = () => {
  const { t } = useTranslation();

  return (
    <header className="flex items-center justify-between px-6 py-4 bg-gray-200 border-b">
      <div className="flex items-center space-x-4">
        <HeaderButton variant="ghost" size="icon" className="md:hidden">
          <Menu className="h-6 w-6" />
          <span className="sr-only">{t("header.toggleMenu")}</span>
        </HeaderButton>
        <Link to="/" className="flex items-center space-x-2">
          <span className="text-2xl font-bold">{t("header.title")}</span>
        </Link>
      </div>
      <div className="flex items-center space-x-4">
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
    </header>
  )
}