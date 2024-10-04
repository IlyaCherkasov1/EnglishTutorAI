import {registerUser} from "@/app/api/identity/identityApi.ts";
import {RegisterSchema, TRegisterSchema} from "@/app/infrastructure/zodSchemas/registerSchema.ts";

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
                return { error: "Registration failed. Please try again." };
            }
        } else {
            return { success: "Registration was successful! Check your email" };
        }
    } catch {
        return { error: "Something went wrong. Please try again." };
    }
}