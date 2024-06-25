'use client';

import React, {useState} from 'react';
import {Input} from '@/app/components/ui/input';
import {Button} from '@/app/components/ui/button';
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
import {RegisterSchema, TRegisterSchema} from "@/app/infrastructure/schemas";

const Register = () => {
    const [error] = useState<string | undefined>("");
    const [success] = useState<string | undefined>("");

    const form = useForm<TRegisterSchema>({
        resolver: zodResolver(RegisterSchema),
        defaultValues: {
            email: "",
            password: "",
            name: "",
        }
    });

    const onSubmit = async (values: TRegisterSchema) => {
        console.log(values);
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
                        <FormField control={form.control} name="name" render={({ field }) => (
                            <FormItem>
                                <FormLabel>Name</FormLabel>
                                <FormControl>
                                    <Input {...field} placeholder="John Doe"/>
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
                        <LogInButton isSubmitting={form.formState.isSubmitting} />
                        <Button className="w-full" variant="outline">
                            Sign Up
                        </Button>
                    </div>
                </div>
            </form>
        </Form>
    );
}

export default Register;