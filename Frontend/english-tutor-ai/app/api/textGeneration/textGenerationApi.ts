import {TextGenerationRequest} from "@/app/dataModels/textGeneration/textGenerationRequest";
import {httpPost} from "@/app/core/requestApi";

const resources = 'textGeneration';

export const correctText = async (textGenerationRequest: TextGenerationRequest)
    : Promise<{ isCorrected: boolean, correctedText: string }> => {
    return httpPost({
        url: `${resources}/correct-text`,
        body: textGenerationRequest,
    });
}