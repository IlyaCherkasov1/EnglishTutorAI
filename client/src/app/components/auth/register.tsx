import {FormProvider, useForm} from "react-hook-form";
import {zodResolver} from "@hookform/resolvers/zod";
import {useState} from "react";
import {FormControl, FormItem, FormLabel, FormMessage} from "@/app/components/ui/form.tsx";
import {Input} from "@/app/components/ui/input.tsx";
import {FormError} from "@/app/components/formStates/form-error.tsx";
import {FormSuccess} from "@/app/components/formStates/form-success.tsx";
import {handleUserRegistration} from "@/app/infrastructure/services/auth/registrationService.ts";
import {Link} from "react-router-dom";
import {SignUpButton} from "@/app/components/buttons/signUpButton.tsx";
import {useTranslation} from "react-i18next";
import {RegisterSchema, TRegisterSchema} from "@/app/infrastructure/validationSchemas/registerSchema.ts";

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
        <FormProvider {...methods}>
            <form onSubmit={handleSubmit(onSubmit)} className="mx-auto max-w-md space-y-6 py-12">
                <div className="space-y-2 text-center">
                    <h1 className="text-3xl font-bold">{t('createAnAccount')}</h1>
                </div>
                <div className="space-y-4">
                    <div className="space-y-2">
                        <FormItem>
                            <FormLabel>{t('name')}</FormLabel>
                            <FormControl>
                                <Input
                                    {...register("firstName")}
                                    disabled={isSubmitting}
                                    placeholder="John Doe" />
                            </FormControl>
                            <FormMessage>{errors.firstName?.message}</FormMessage>
                        </FormItem>
                    </div>
                    <div className="space-y-2">
                        <FormItem>
                            <FormLabel>{t('email')}</FormLabel>
                            <FormControl>
                                <Input {...register("email")}
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
                                <Input {...register("password")}
                                       disabled={isSubmitting}
                                       placeholder="*******"
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
                <p className="mt-2 text-center text-sm text-gray-500">
                    {t('alreadyHaveAccount')}
                    <span className="mr-1"></span>
                    <Link to={"/auth/login"} className="font-semibold leading-6 text-indigo-600 hover:text-indigo-500">
                        Login
                    </Link>
                </p>
            </form>
        </FormProvider>
    );
}

export default Register;