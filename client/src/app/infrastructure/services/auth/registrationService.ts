import {registerUser} from "@/app/api/identityApi.ts";
import {RegisterSchema, TRegisterSchema} from "@/app/infrastructure/validationSchemas/registerSchema.ts";
import {getLocalizedMessage} from "@/app/infrastructure/utils/localizerUtils.ts";

export const handleUserRegistration = async (values: TRegisterSchema) => {
    const validatedFields = RegisterSchema.safeParse(values);

    if (!validatedFields.success) {
        return { error: "Invalid fields!" };
    }

    const { firstName, email, password } = validatedFields.data;

    try {
        const response = await registerUser({ firstName, email, password });

        if (!response.isSucceeded) {
            const errors = response.errors || [];

            if (errors.length > 0) {
                return { error: errors[0] };
            } else {
                return { error: getLocalizedMessage('registrationFailed') };
            }
        } else {
            return { success: getLocalizedMessage('registrationSuccessful') };
        }
    } catch {
        return { error: getLocalizedMessage('somethingWentWrong')  };
    }
}