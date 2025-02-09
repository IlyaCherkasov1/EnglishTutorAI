import {defineConfig} from 'vite';
import react from '@vitejs/plugin-react'
import fs from 'fs';
import path from 'path';
import checker from "vite-plugin-checker";

const isProduction = process.env.NODE_ENV === 'production';

export default defineConfig({
    base: '/',
    plugins: [react(), checker({ typescript: true })],
    server: {
        https: !isProduction
            ? {
                key: fs.readFileSync(path.resolve(__dirname, 'key.pem')),
                cert: fs.readFileSync(path.resolve(__dirname, 'cert.pem')),
            }
            : undefined,
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