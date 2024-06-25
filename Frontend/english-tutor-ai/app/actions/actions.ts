'use server'

import {addDocument} from "@/app/api/document/documentApi";
import {correctText} from "@/app/api/languageModel/languageModelApi";
import {signIn} from "@/auth";
import {AuthError} from "next-auth";
import {LoginSchema, TLoginSchema} from "@/app/infrastructure/schemas";

export const addDocumentAction = async (formData: FormData) => {
    const title = formData.get('document-title') as string;
    const content = formData.get('document-input') as string;

    await addDocument({ title: title, content: content });
}

export interface handleCorrectionParams {
    translatedText: string;
    currentLine: string;
    threadId: string;
}

export const handleCorrection = async (props: handleCorrectionParams)
    : Promise<{ isCorrected: boolean, correctedText: string }> => {

    const { isCorrected, correctedText } = await correctText({
        originalText: props.currentLine,
        translatedText: props.translatedText,
        threadId: props.threadId,
    })

    return { isCorrected, correctedText };
}

export const loginAction = async (values: TLoginSchema) => {
    const validatedFields = LoginSchema.safeParse(values);

    if (!validatedFields.success) {
        return { error: "Invalid fields!" };
    }

    const { email, password } = validatedFields.data;

    try {
        await signIn("credentials", {
            email, password, redirectTo: "/settings"
        });
        return { success: "Success!" };
    } catch (error) {
        if (error instanceof AuthError) {
            switch (error.type) {
                case "CredentialsSignin":
                    return { error: "Invalid Credentials" }
                case "AccessDenied":
                    return { error: "Access Denied" };
                default:
                    return { error: "Something went wrong" };
            }
        }

        throw error;
    }
}