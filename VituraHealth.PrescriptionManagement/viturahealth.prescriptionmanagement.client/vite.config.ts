import { fileURLToPath, URL } from 'node:url';

import { defineConfig } from 'vite';
import plugin from '@vitejs/plugin-react';

// Set the target to HTTP on port 5299
const target = 'http://localhost:5300';

// https://vitejs.dev/config/
export default defineConfig({
    plugins: [plugin()],
    resolve: {
        alias: {
            '@': fileURLToPath(new URL('./src', import.meta.url))
        }
    },
    server: {
        proxy: {
            '^/viturahealth': {
                target,
                secure: false
            }
        },
        port: 5300,
        // Remove the https property to use HTTP
    }
})