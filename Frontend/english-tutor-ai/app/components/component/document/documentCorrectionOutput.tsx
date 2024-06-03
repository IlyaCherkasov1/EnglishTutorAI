import React from 'react';
import {useI18n} from "@/app/locales/client";
import DiffComponent from "@/app/components/component/DiffComponent";

interface Props {
    originalText: string;
    correctedText: string;
    isCorrected: boolean;
}

const DocumentCorrectionOutput = (props: Props) => {
    const t = useI18n()

    return (
        <div>
            <div className="bg-gray-100 p-4 rounded-md">
                {props.isCorrected ? (
                    <>
                        <h3 className="font-bold mb-2">{t('correctedText')}:</h3>
                        <DiffComponent originalText={props.originalText} correctedText={props.correctedText} />
                    </>
                ) : (
                    <h3 className="font-bold mb-2">{t('translationSuccessMessage')}</h3>
                )}
            </div>
        </div>
    );
};

export default DocumentCorrectionOutput;