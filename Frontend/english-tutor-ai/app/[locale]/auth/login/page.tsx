'use client';

import React, {useState} from 'react';
import {Input} from '@/app/components/ui/input';
import {LogInButton} from "@/app/components/component/buttons/logInButton";
import {useForm} from "react-hook-form";
import {zodResolver} from "@hookform/resolvers/zod"
import {
    Form,
    FormItem,
    FormLabel,
    FormControl,
    FormDescription,
    FormMessage,
    FormField,
} from "@/app/components/ui/form";
import {FormError} from "@/app/components/component/form-error";
import {FormSuccess} from "@/app/components/component/form-success";
import {LoginSchema, TLoginSchema} from "@/app/infrastructure/schemas";
import {loginAction} from "@/app/actions/actions";

const Login = () => {
    const [error, setError] = useState<string | undefined>("");
    const [success, setSuccess] = useState<string | undefined>("");

    const form = useForm<TLoginSchema>({
        resolver: zodResolver(LoginSchema),
        defaultValues: {
            email: "",
            password: "",
        }
    });

    const onSubmit = async (values: TLoginSchema) => {
        setError("");
        setSuccess("");

        await loginAction(values).then((data) => {
                setError(data?.error);
                setSuccess(data?.success);
            }
        )
    }

    return (
        <Form {...form}>
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
                                    <Input {...field} placeholder="john.doe@example.com" type="email"/>
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
                                    <Input {...field} placeholder="*******" type="password"/>
                                </FormControl>
                                <FormMessage/>
                            </FormItem>
                        )}/>
                    </div>
                    <FormError message={error}/>
                    <FormSuccess message={success}/>
                    <div className="grid gap-4">
                        <LogInButton isSubmitting={form.formState.isSubmitting}/>
                    </div>
                </div>
                <p className="mt-2 text-center text-sm text-gray-500">
                    Do not have an account?
                    <a href="#" className="font-semibold leading-6 text-indigo-600 hover:text-indigo-500"> Register</a>
                </p>
            </form>
        </Form>
    );
}

export default Login;