import {createRoot} from 'react-dom/client'
import '@/index.css'
import '@/i18n.ts'
import App from "@/app.tsx";
import {ErrorBoundary} from "@/app/components/errorBoundary.tsx";

createRoot(document.getElementById('root')!).render(
    <ErrorBoundary >
        <App />
    </ErrorBoundary>
)