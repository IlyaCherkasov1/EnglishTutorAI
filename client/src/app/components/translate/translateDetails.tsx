import {getTranslateDetails} from "@/app/api/translateApi.ts";
import {TranslateDetail} from "@/app/components/translate/translateDetail.tsx";
import {useState} from "react";
import {TranslateDetailsModel} from "@/app/dataModels/translate/translateDetailsModel.ts";
import {useParams} from "react-router-dom";
import useAsyncEffect from "use-async-effect";
import {ContentLoaderSpinner} from "@/app/components/ui/contentLoaderSpinner.tsx";

const TranslateDetails = () => {
    const { translateId } = useParams<{ translateId: string }>();
    const [translate, setTranslate] = useState<TranslateDetailsModel>();

    useAsyncEffect(async () => {
        const translateData = await getTranslateDetails(translateId!);

        setTranslate(translateData);
    }, [translateId]);

    if (!translate || translate.sentences.length === 0) {
        return <ContentLoaderSpinner />;
    }

    return (
        <div className="flex flex-col h-screen">
            <TranslateDetail translateDetails={translate} />
        </div>
    )
};

export default TranslateDetails;