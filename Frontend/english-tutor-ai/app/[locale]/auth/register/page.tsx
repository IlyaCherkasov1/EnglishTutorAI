'use client';

import React, {useState} from 'react';
import {Input} from '@/app/components/ui/input';
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
import {RegisterSchema, TRegisterSchema} from "@/app/infrastructure/schemas";
import {SignUpButton} from "@/app/components/component/buttons/signUpButton";
import {RegisterAction} from "@/app/actions/authAction";

const Register = () => {
    const [error, setError] = useState<string | undefined>("");
    const [success, setSuccess] = useState<string | undefined>("");

    const form = useForm<TRegisterSchema>({
        resolver: zodResolver(RegisterSchema),
        defaultValues: {
            userName: "",
            email: "",
            password: "",
        }
    });

    const onSubmit = async (values: TRegisterSchema) => {
        setError("");
        setSuccess("");

        await RegisterAction(values).then((data) => {
                setError(data?.error);
                setSuccess(data?.success);
            }
        )
    }

    return (
        <Form {...form}>
            <form onSubmit={form.handleSubmit(onSubmit)} className="mx-auto max-w-md space-y-6 py-12">
                <div className="space-y-2 text-center">
                    <h1 className="text-3xl font-bold">Create an account</h1>
                    <FormDescription>Sign up or log in to continue</FormDescription>
                </div>
                <div className="space-y-4">
                    <div className="space-y-2">
                        <FormField control={form.control} name="userName" render={({ field }) => (
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
                    <a href="/auth/login" className="font-semibold leading-6 text-indigo-600 hover:text-indigo-500">
                        Login
                    </a>
                </p>
            </form>
        </Form>
    );
}

export default Register;