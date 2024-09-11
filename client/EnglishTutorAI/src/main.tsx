import {createRoot} from 'react-dom/client'
import App from './app.tsx'
import './index.css'
import {BrowserRouter} from "react-router-dom";
import './i18n.ts'

createRoot(document.getElementById('root')!).render(
    <BrowserRouter>
        <App/>
    </BrowserRouter>
)