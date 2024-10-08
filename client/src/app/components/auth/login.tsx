import {FormProvider, useForm} from "react-hook-form";
import {zodResolver} from "@hookform/resolvers/zod";
import {useEffect, useState} from "react";
import {FormControl, FormDescription, FormItem, FormLabel, FormMessage} from "@/app/components/ui/form.tsx";
import {Input} from "@/app/components/ui/input.tsx";
import {FormError} from "@/app/components/formStates/form-error.tsx";
import {SignInButton} from "@/app/components/buttons/signInButton.tsx";
import {Link, useNavigate} from "react-router-dom";
import {routeLinks} from "@/app/components/layout/routes/routeLink.ts";
import {applyNewIdentity} from "@/app/infrastructure/services/auth/identityService.ts";
import {login} from "@/app/infrastructure/services/auth/loginService.ts";
import {GoogleSignInButton} from "@/app/components/auth/googleSignInButton.tsx";
import {FacebookSignInButton} from "@/app/components/auth/facebookSignInButton.tsx";
import {contextStore} from "@/app/infrastructure/stores/contextStore.ts";
import {useTranslation} from "react-i18next";
import {LoginSchema, TLoginSchema} from "@/app/infrastructure/validationSchemas/loginSchema.ts";

const Login = () => {
    const [error, setError] = useState<string | undefined>("");
    const navigate = useNavigate();
    const { t } = useTranslation();

    const methods = useForm<TLoginSchema>({
        resolver: zodResolver(LoginSchema),
    });

    const { handleSubmit, register, formState: { errors, isSubmitting } } = methods;

    useEffect(() => {
        if (contextStore.isAuthenticated) {
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
            <form onSubmit={handleSubmit(onSubmit)} className="mx-auto max-w-md space-y-6 py-12">
                <div className="space-y-2 text-center">
                    <h1 className="text-3xl font-bold">{t('welcome')}</h1>
                    <FormDescription>{t('signUpOrLogInToContinue')}</FormDescription>
                </div>
                <div className="space-y-4">
                    <div className="space-y-2">
                        <FormItem>
                            <FormLabel>{t('email')}</FormLabel>
                            <FormControl>
                                <Input
                                    {...register("email")}
                                    name="email"
                                    disabled={isSubmitting}
                                    placeholder="john.doe@example.com"
                                    type="email" />
                            </FormControl>
                            <FormMessage>{errors.email?.message}</FormMessage>
                        </FormItem>
                    </div>
                    <div className="space-y-2">
                        <FormItem>
                            <FormLabel>{t('password')}</FormLabel>
                            <FormControl>
                                <Input
                                    {...register("password")}
                                    name="password"
                                    disabled={isSubmitting}
                                    placeholder="*******"
                                    type="password" />
                            </FormControl>
                            <FormMessage>{errors.password?.message}</FormMessage>
                        </FormItem>
                    </div>
                    <FormError message={error} />
                    <div className="grid gap-4">
                        <SignInButton isSubmitting={isSubmitting} />
                        <GoogleSignInButton />
                        <FacebookSignInButton />
                    </div>
                </div>
                <p className="mt-2 text-center text-sm text-gray-500">
                    {t('doNotHaveAccount')}
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