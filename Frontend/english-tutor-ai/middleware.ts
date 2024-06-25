import {chain} from "@/middlewares/chain";
import {localizationMiddleware} from "@/middlewares/I18nMiddleware";
import {authMiddleware} from "@/middlewares/authMiddleware";

export default chain([authMiddleware, localizationMiddleware])

export const config = {
    matcher: ["/((?!api|_next/static|_next/image|favicon.ico).*)"],
};