module.exports = {
    content: [
        "./app/**/*.{js,ts,jsx,tsx,mdx}",
    ],
    theme: {
        extend: {
            colors: {
                destructive: '#FF0000',
            },
        },
    },
    plugins: [require("tailwindcss-animate")],
}