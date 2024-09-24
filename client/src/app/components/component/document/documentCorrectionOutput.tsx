import {useTranslation} from "react-i18next";
import DiffComponent from "@/app/components/component/diffComponent.tsx";

interface Props {
    originalText: string;
    correctedText: string;
    isCorrected: boolean;
}

const DocumentCorrectionOutput = (props: Props) => {
    const { t } = useTranslation();

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