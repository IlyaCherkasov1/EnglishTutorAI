import {FormProvider, useForm} from "react-hook-form";
import {zodResolver} from "@hookform/resolvers/zod";
import {useEffect, useState} from "react";
import {FormControl, FormDescription, FormItem, FormMessage} from "@/app/components/ui/form.tsx";
import {Input} from "@/app/components/ui/input.tsx";
import {FormError} from "@/app/components/formStates/form-error.tsx";
import {SignInButton} from "@/app/components/buttons/signInButton.tsx";
import {Link, useNavigate} from "react-router-dom";
import {routes} from "@/app/components/layout/routes/routeLink.ts";
import {applyNewIdentity} from "@/app/infrastructure/services/auth/identityService.ts";
import {login} from "@/app/infrastructure/services/auth/loginService.ts";
import {GoogleSignInButton} from "@/app/components/auth/googleSignInButton.tsx";
import {FacebookSignInButton} from "@/app/components/auth/facebookSignInButton.tsx";
import {contextStore} from "@/app/infrastructure/stores/contextStore.ts";
import {useTranslation} from "react-i18next";
import {LoginSchema, TLoginSchema} from "@/app/infrastructure/validationSchemas/loginSchema.ts";
import {languageMap} from "@/app/dataModels/languageMap.ts";

const Login = () => {
    const [error, setError] = useState<string | undefined>("");
    const navigate = useNavigate();
    const { t, i18n } = useTranslation();

    const methods = useForm<TLoginSchema>({
        resolver: zodResolver(LoginSchema),
    });

    const { handleSubmit, register, formState: { errors, isSubmitting } } = methods;

    useEffect(() => {
        if (contextStore.isAuthenticated) {
            navigate(routes.translates, { replace: true })
        }
    }, [navigate]);

    const onSubmit = async (values: TLoginSchema) => {
        setError("");

        const result = await login(values);

        if (result.isSuccess) {
            await applyNewIdentity(result.data);
            await i18n.changeLanguage(languageMap[contextStore.language]);
            navigate(routes.translates, { replace: true });
        } else {
            setError(result.error);
        }
    }

    return (
        <div className="flex items-center justify-center min-h-screen">
            <FormProvider {...methods}>
                <form onSubmit={handleSubmit(onSubmit)} className="w-full max-w-md">
                    <div className="space-y-6">
                        <div className="space-y-2 text-center">
                            <h1 className="text-3xl font-bold">{t('authentication')}</h1>
                            <FormDescription>{t('signUpOrLogInToContinue')}</FormDescription>
                        </div>
                        <div className="space-y-4">
                            <FormItem>
                                <FormControl>
                                    <Input
                                        {...register("email")}
                                        name="email"
                                        disabled={isSubmitting}
                                        placeholder={t('email')}
                                        type="email" />
                                </FormControl>
                                <FormMessage>{errors.email?.message}</FormMessage>
                            </FormItem>
                            <FormItem>
                                <FormControl>
                                    <Input
                                        {...register("password")}
                                        name="password"
                                        disabled={isSubmitting}
                                        placeholder={t('password')}
                                        type="password" />
                                </FormControl>
                                <FormMessage>{errors.password?.message}</FormMessage>
                            </FormItem>
                            <FormError message={error} />
                            <div className="space-y-4">
                                <SignInButton isSubmitting={isSubmitting} />
                                <div className="flex items-center justify-center space-x-6">
                                    <GoogleSignInButton />
                                    <FacebookSignInButton />
                                </div>
                            </div>
                        </div>
                    </div>
                    <p className="mt-16 text-center text-sm text-gray-500">
                        {t('doNotHaveAccount')}
                        <Link to={routes.register}
                              className="font-semibold text-black ml-2">
                            {t('register')}
                        </Link>
                    </p>
                </form>
            </FormProvider>
        </div>
    );
}

export default Login;