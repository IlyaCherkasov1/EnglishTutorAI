import {FormProvider, useForm} from "react-hook-form";
import {zodResolver} from "@hookform/resolvers/zod";
import {useState} from "react";
import {FormControl, FormDescription, FormField, FormItem, FormLabel, FormMessage} from "../components/ui/form.tsx";
import {Input} from "../components/ui/input.tsx";
import {FormError} from "../components/component/form-error.tsx";
import {RegisterSchema, TRegisterSchema} from "../infrastructure/schemas";
import {FormSuccess} from "../components/component/form-success.tsx";
import {SignUpButton} from "../components/component/buttons/signUpButton.tsx";
import {register} from "../infrastructure/services/auth/registrationService.ts";
import {Link} from "react-router-dom";

const Register = () => {
    const [error, setError] = useState<string | undefined>("");
    const [success, setSuccess] = useState<string | undefined>("");
    const methods = useForm();

    const form = useForm<TRegisterSchema>({
        resolver: zodResolver(RegisterSchema),
        defaultValues: {
            firstName: "",
            email: "",
            password: "",
        }
    });

    const onSubmit = async (values: TRegisterSchema) => {
        setError("");
        setSuccess("");

        await register(values).then((data) => {
                setError(data?.error);
                setSuccess(data?.success);
            }
        )
    }

    return (
        <FormProvider {...methods}>
            <form onSubmit={form.handleSubmit(onSubmit)} className="mx-auto max-w-md space-y-6 py-12">
                <div className="space-y-2 text-center">
                    <h1 className="text-3xl font-bold">Create an account</h1>
                    <FormDescription>Sign up or log in to continue</FormDescription>
                </div>
                <div className="space-y-4">
                    <div className="space-y-2">
                        <FormField control={form.control} name="firstName" render={({ field }) => (
                            <FormItem>
                                <FormLabel>Name</FormLabel>
                                <FormControl>
                                    <Input {...field} disabled={form.formState.isSubmitting} placeholder="John Doe"/>
                                </FormControl>
                                <FormMessage/>
                            </FormItem>
                        )}/>
                    </div>
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
                    <FormSuccess message={success}/>
                    <div className="grid gap-4">
                        <SignUpButton isSubmitting={form.formState.isSubmitting}/>
                    </div>
                </div>
                <p className="mt-2 text-center text-sm text-gray-500">
                    Already have an account?
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