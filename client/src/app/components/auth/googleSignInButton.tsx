import {loginWithExternalProvider} from "@/app/infrastructure/utils/authUtils.ts";

export const GoogleSignInButton = () => {
    return (
        <button type="button"
                onClick={() => loginWithExternalProvider('Google')}
                className="inline-flex items-center justify-center w-9 h-9 bg-white rounded-full hover:bg-gray-100 ">
            <img src="/google_logo.svg"
                 alt="Google sign-in"
                 width="30"
                 height="30"/>
        </button>
    );
};