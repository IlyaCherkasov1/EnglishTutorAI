import {FormProvider, useForm} from "react-hook-form";
import {LoginSchema, TLoginSchema} from "@/app/infrastructure/schemas";
import {zodResolver} from "@hookform/resolvers/zod";
import {useEffect, useState} from "react";
import {FormControl, FormDescription, FormField, FormItem, FormLabel, FormMessage} from "@/app/components/ui/form.tsx";
import {Input} from "@/app/components/ui/input.tsx";
import {FormError} from "@/app/components/component/form-error.tsx";
import {SignInButton} from "@/app/components/component/buttons/signInButton.tsx";
import {Link, useNavigate} from "react-router-dom";
import {routeLinks} from "@/app/components/layout/routes/routeLink.ts";
import {applyNewIdentity} from "@/app/infrastructure/services/auth/identityService.ts";
import {login} from "@/app/infrastructure/services/auth/loginService.ts";
import {GoogleSignInButton} from "@/app/components/component/auth/googleSignInButton.tsx";
import {isAccessTokenValid} from "@/app/infrastructure/utils/tokenUtils.ts";
import {FacebookSignInButton} from "@/app/components/component/auth/facebookSignInButton.tsx";

const Login = () => {
    const [error, setError] = useState<string | undefined>("");
    const navigate = useNavigate();
    const methods = useForm();

    const form = useForm<TLoginSchema>({
        resolver: zodResolver(LoginSchema),
        defaultValues: {
            email: "",
            password: "",
        }
    });

    useEffect(() => {
        if (isAccessTokenValid()) {
            navigate(routeLinks.home, { replace: true })
        }
    }, [navigate]);

    const onSubmit = async (values: TLoginSchema) => {
        setError("");

        const result = await login(values);

        if (result.isSuccess) {
            await applyNewIdentity(result.data);
            navigate(routeLinks.home, { replace: true });
        } else {
            setError(result.error);
        }
    }

    return (
        <FormProvider {...methods}>
            <form onSubmit={form.handleSubmit(onSubmit)} className="mx-auto max-w-md space-y-6 py-12">
                <div className="space-y-2 text-center">
                    <h1 className="text-3xl font-bold">Welcome</h1>
                    <FormDescription>Sign up or log in to continue</FormDescription>
                </div>
                <div className="space-y-4">
                    <div className="space-y-2">
                        <FormField control={form.control} name="email" render={({ field }) => (
                            <FormItem>
                                <FormLabel>Email</FormLabel>
                                <FormControl>
                                    <Input {...field}
                                           disabled={form.formState.isSubmitting}
                                           placeholder="john.doe@example.com"
                                           type="email"/>
                                </FormControl>
                                <FormMessage/>
                            </FormItem>
                        )}/>
                    </div>
                    <div className="space-y-2">
                        <FormField control={form.control} name="password" render={({ field }) => (
                            <FormItem>
                                <FormLabel>Password</FormLabel>
                                <FormControl>
                                    <Input {...field}
                                           disabled={form.formState.isSubmitting}
                                           placeholder="*******"
                                           type="password"/>
                                </FormControl>
                                <FormMessage/>
                            </FormItem>
                        )}/>
                    </div>
                    <FormError message={error}/>
                    <div className="grid gap-4">
                        <SignInButton isSubmitting={form.formState.isSubmitting} />
                        <GoogleSignInButton />
                        <FacebookSignInButton />
                    </div>
                </div>
                <p className="mt-2 text-center text-sm text-gray-500">
                    Do not have an account?
                    <span className="mr-1"></span>
                    <Link to="/auth/register" className="font-semibold leading-6 text-indigo-600 hover:text-indigo-500">
                        Register
                    </Link>
                </p>
            </form>
        </FormProvider>
    );
}

export default Login;