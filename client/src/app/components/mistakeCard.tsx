import {Card, CardContent, CardFooter, CardHeader} from "@/app/components/ui/card.tsx";
import DiffComponent from "@/app/components/chatBot/diffComponent.tsx";
import {formatDateToISO} from "@/app/infrastructure/helpers/dateHelpers.ts";
import {useTranslation} from "react-i18next";
import {Link} from "react-router-dom";
import { ArrowRight } from "lucide-react";

interface Props {
    id: string;
    translatedText: string;
    correctedText: string;
    createdAt?: string;
    translateId?: string;
}

export const MistakeCard = (props: Props) => {
    const { t } = useTranslation();

    return (
        <Card className="w-full mb-4 hover:shadow-lg transition-shadow border-2 border-gray-300" key={props.id}>
            <CardHeader>
                <div className="p-3 bg-gray-200 rounded-md">
                    <h3 className="text-sm font-medium text-gray-900 mb-1">{t('translatedText')}</h3>
                    {props.translatedText}
                </div>
            </CardHeader>
            <CardContent>
                <div className="p-3 bg-gray-200 rounded-md">
                    <h3 className="text-sm font-medium text-gray-900 mb-1">{t('correctedText')}</h3>
                    <DiffComponent
                        translatedText={props.translatedText}
                        correctedText={props.correctedText} />
                </div>
            </CardContent>
            {props.createdAt &&
                <CardFooter>
                    <p className="text-sm text-gray-500">{formatDateToISO(props.createdAt)}</p>
                </CardFooter>}
            {props.translateId && (
                <Link to={`/translates/${props.translateId}`}
                      className="absolute bottom-4 right-4 flex items-center gap-2 px-4 py-2">
                    {t("goToTranslate")}
                    <ArrowRight size={16} />
                </Link>
            )}
        </Card>
    );
}