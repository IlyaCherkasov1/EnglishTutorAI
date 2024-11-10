import {defineConfig} from 'vite';
import react from '@vitejs/plugin-react'
import fs from 'fs';
import path from 'path';
import checker from "vite-plugin-checker";

export default defineConfig({
    plugins: [react(), checker({ typescript: true })],
    server: {
        https: {
            key: fs.readFileSync(path.resolve(__dirname, 'key.pem')),
            cert: fs.readFileSync(path.resolve(__dirname, 'cert.pem')),
        }
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