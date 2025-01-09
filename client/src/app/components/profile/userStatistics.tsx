import {useTranslation} from "react-i18next";

interface Props {
    correctedMistakes: number;
}

export const UserStatistics = (props: Props) => {
    const { t } = useTranslation();

    return (
        <div className="mb-14 rounded-lg">
            <h2 className="text-xl font-semibold text-gray-800 mb-4">{t('statistics')}</h2>
            <div className="grid grid-cols-2 gap-4">
                <div className="flex items-center justify-between border rounded-lg p-4 bg-gray-100">
                    <div className="flex items-center">
                        <div className="w-10 h-10 flex items-center justify-center rounded-full">
                            <img
                                src="/mistake_statistic.png"
                                alt="Streak"
                                className="w-8 h-8"
                            />
                        </div>
                        <div className="ml-4">
                            <p className="text-lg font-bold text-gray-800">{props.correctedMistakes}</p>
                            <p className="text-sm text-gray-500">{t('correctedMistakes')}</p>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    )
}