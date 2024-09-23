import {routeLinks} from "@/app/components/layout/routes/routeLink.ts";

export const isAuthPage = (pathname: string) => {
    return pathname === routeLinks.login || pathname === routeLinks.register;
}