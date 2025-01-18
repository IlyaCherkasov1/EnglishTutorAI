import {defineConfig} from 'vite';
import react from '@vitejs/plugin-react'
import path from 'path';
import checker from "vite-plugin-checker";

export default defineConfig({
    plugins: [react(), checker({ typescript: true })],
    server: {
    },
    resolve: {
        alias: {
            "@": path.resolve(__dirname, "./src"),
        },
    },
    css: {
        preprocessorOptions: {
            scss: {
                api: 'modern-compiler',
            },
        },
    },
});