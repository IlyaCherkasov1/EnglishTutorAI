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
    document: {
        documentDetails: "/documents/:documentId"
    },
    profile: "/profile",
};