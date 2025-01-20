import {defineConfig} from 'vite';
import react from '@vitejs/plugin-react'
import fs from 'fs';
import path from 'path';
import checker from "vite-plugin-checker";

const isCI = process.env.CI === 'true';

export default defineConfig({
    base: '/',
    plugins: [react(), checker({ typescript: true })],
    build: {
        rollupOptions: {
            output: {
                manualChunks: undefined,
            },
        },
    },
    server: {
        https: undefined,
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