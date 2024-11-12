import {getContext} from "@/app/api/contextApi.ts";
import {contextStore} from "@/app/infrastructure/stores/contextStore.ts";

export const contextService = {
    load: async () => {
        const context = await getContext();
        contextStore.setContext(context);
    }
}