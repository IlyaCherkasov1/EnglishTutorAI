import {createRoot} from 'react-dom/client'
import './index.css'
import './i18n.ts'
import {MainComponent} from "./app/infrastructure/common/mainComponent.tsx";

createRoot(document.getElementById('root')!).render(
    <MainComponent />
)