import { ArrowLeft } from 'lucide-react';
import { Button } from "../../ui/button.tsx";
import { useTranslation } from "react-i18next";
import { useNavigate } from 'react-router-dom';

const BackButton = () => {
    const navigate = useNavigate();
    const { t } = useTranslation();

    return (
        <Button className="inline-flex items-center" variant="outline" onClick={() => navigate(-1)}>
            <ArrowLeft className="mr-2 h-4 w-4" />
            {t('back')}
        </Button>
    );
};

export default BackButton;