import {auth} from "@/auth";

export const getAccessToken = async (): Promise<string | undefined> => {
    const session = await auth();

    return session?.user.accessToken;
};