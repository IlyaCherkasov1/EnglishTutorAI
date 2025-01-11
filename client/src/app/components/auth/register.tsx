import {FormProvider, useForm} from "react-hook-form";
import {zodResolver} from "@hookform/resolvers/zod";
import {useState} from "react";
import {FormControl, FormItem, FormMessage} from "@/app/components/ui/form.tsx";
import {Input} from "@/app/components/ui/input.tsx";
import {FormError} from "@/app/components/formStates/form-error.tsx";
import {FormSuccess} from "@/app/components/formStates/form-success.tsx";
import {handleUserRegistration} from "@/app/infrastructure/services/auth/registrationService.ts";
import {Link} from "react-router-dom";
import {SignUpButton} from "@/app/components/buttons/signUpButton.tsx";
import {useTranslation} from "react-i18next";
import {RegisterSchema, TRegisterSchema} from "@/app/infrastructure/validationSchemas/registerSchema.ts";
import {routes} from "@/app/components/layout/routes/routeLink.ts";

const Register = () => {
    const [error, setError] = useState<string | undefined>("");
    const [success, setSuccess] = useState<string | undefined>("");
    const { t } = useTranslation();

    const methods = useForm<TRegisterSchema>({
        resolver: zodResolver(RegisterSchema),
    });

    const { handleSubmit, register, formState: { errors, isSubmitting } } = methods;

    const onSubmit = async (values: TRegisterSchema) => {
        setError("");
        setSuccess("");

        await handleUserRegistration(values).then((data) => {
                setError(data?.error);
                setSuccess(data?.success);
            }
        )
    }

    return (
        <div className="flex items-center justify-center min-h-screen">
            <FormProvider {...methods}>
                <form onSubmit={handleSubmit(onSubmit)} className="w-full max-w-md">
                    <div className="space-y-6">
                        <div className="space-y-2 text-center">
                            <h1 className="text-3xl font-bold">{t('createAnAccount')}</h1>
                        </div>
                        <div className="space-y-4">
                            <div className="space-y-2">
                                <FormItem>
                                    <FormControl>
                                        <Input
                                            {...register("firstName")}
                                            disabled={isSubmitting}
                                            placeholder={t('name')} />
                                    </FormControl>
                                    <FormMessage>{errors.firstName?.message}</FormMessage>
                                </FormItem>
                            </div>
                            <div className="space-y-2">
                                <FormItem>
                                    <FormControl>
                                        <Input {...register("email")}
                                               disabled={isSubmitting}
                                               placeholder={t('email')}
                                               type="email" />
                                    </FormControl>
                                    <FormMessage>{errors.email?.message}</FormMessage>
                                </FormItem>
                            </div>
                            <div className="space-y-2">
                                <FormItem>
                                    <FormControl>
                                        <Input {...register("password")}
                                               disabled={isSubmitting}
                                               placeholder={t('password')}
                                               type="password" />
                                    </FormControl>
                                    <FormMessage>{errors.password?.message}</FormMessage>
                                </FormItem>
                            </div>
                            <FormError message={error} />
                            <FormSuccess message={success} />
                            <div className="grid gap-4">
                                <SignUpButton isSubmitting={isSubmitting} />
                            </div>
                        </div>
                    </div>
                    <p className="mt-16 text-center text-sm text-gray-500">
                        {t('alreadyHaveAccount')}
                        <Link to={routes.login} className="font-semibold text-black ml-2">
                            {t('login')}
                        </Link>
                    </p>
                </form>
            </FormProvider>
        </div>
    );
}

export default Register;