export const routes = {
    home: "/",
    history: "/history",
    accessDenied: "/access-denied",
    adminPanel: "/adminPanel",
    login: "/auth/login",
    register: "/auth/register",
    errors: {
        forbidden: "/errors/forbidden",
        notFound: "/errors/not-found",
    },
    translate: {
        translateDetails: "/translates/:translateId"
    },
    profile: "/profile",
    completedTranslates: "/completedTranslates",
};