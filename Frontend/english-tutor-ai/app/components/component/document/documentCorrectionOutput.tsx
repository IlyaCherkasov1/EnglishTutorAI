import React from 'react';
import {Button} from "@/app/components/ui/button"
import {useI18n} from "@/app/locales/client";
import {MessageCircle} from "lucide-react";

interface Props {
    correctedText: string;
    translatedText: string;
}

const DocumentCorrectionOutput = (props: Props) => {
    const t = useI18n()

    return (
        <div>
            <div className="bg-gray-100 p-4 rounded-md">
                <h3 className="font-bold mb-2">{t('correctedText')}:</h3>
                <p className="mb-4 text-sm">
                    {props.correctedText}
                </p>
                <h3 className="font-bold mb-2">{t('yourTranslation')}:</h3>
                <p className="text-sm">
                    {props.translatedText}
                </p>
            </div>
            <div className="p-4 bg-white shadow fixed bottom-0 left-0 right-0">
                <Button className="rounded-full px-4 py-2" variant="secondary">
                    <MessageCircle className="mr-2 h-5 w-5" />
                    {t('askAI')}
                </Button>
            </div>
        </div>
    );
};

export default DocumentCorrectionOutput;