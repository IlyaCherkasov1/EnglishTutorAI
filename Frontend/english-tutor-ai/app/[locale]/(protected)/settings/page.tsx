import React from 'react';
import {auth, signOut} from "@/auth";
import {Button} from "@/app/components/ui/button";

const Page = async () => {
    const session = await auth();

    return (
        session?.user ? (
            <form action={async () => {
                "use server"
                await signOut()
            }}>
                <div>{JSON.stringify(session)}</div>
                <Button variant="outline" type="submit">Sign Out</Button>
            </form>
        ) : (
            <div>private</div>
        )
    );
};

export default Page;