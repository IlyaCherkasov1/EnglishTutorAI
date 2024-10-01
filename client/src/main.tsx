import {createRoot} from 'react-dom/client'
import '@/index.css'
import '@/i18n.ts'
import App from "@/app.tsx";

createRoot(document.getElementById('root')!).render(
    <App/>
)