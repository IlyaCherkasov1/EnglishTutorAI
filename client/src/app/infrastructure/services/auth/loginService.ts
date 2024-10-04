import {LoginSchema, TLoginSchema} from "@/app/infrastructure/zodSchemas/loginSchema.ts";
import {loginUser} from "@/app/api/identity/identityApi.ts";

type LoginResult =
    | { isSuccess: true; data: string }
    | { isSuccess: false; error: string };

export async function login(values: TLoginSchema): Promise<LoginResult> {
    const validatedFields = LoginSchema.safeParse(values);

    if (!validatedFields.success) {
        return { isSuccess: false, error: "Invalid fields!" };
    }

    const { email, password } = validatedFields.data;

    const response = await loginUser({
        email,
        password,
    });

    return response.isSucceeded
        ? { isSuccess: true, data: response.data }
        : { isSuccess: false, error: response.errors[0] };
}