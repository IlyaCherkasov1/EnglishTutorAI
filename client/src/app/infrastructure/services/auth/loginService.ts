import {LoginSchema, TLoginSchema} from "../../schemas";
import {loginUser} from "../../../api/identity/identityApi.ts";
import {LoginResponse} from "../../../dataModels/identity/loginResponse.ts";

type LoginResult =
    | { isSuccess: true; data: LoginResponse }
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