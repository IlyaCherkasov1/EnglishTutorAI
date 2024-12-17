import TextareaAutosize from "react-textarea-autosize";
import { MoveUp } from "lucide-react";
import {useEnterSubmit} from "@/hooks/useEnterSubmit.ts";
import {useForm} from "react-hook-form";
import {useTranslation} from "react-i18next";
import {Button} from "@/app/components/ui/button.tsx";

interface Props {
    onSendMessage: (message: string) => Promise<void>;
}

export const ChatInput = (props: Props) => {
    const { register, handleSubmit, reset, formState: { isSubmitting }, watch } = useForm<{ message: string }>();
    const { t } = useTranslation();

    const message = watch("message", "");

    const onSubmit = async ({ message }: { message: string }) => {
        reset();
        await props.onSendMessage(message);
    };

    return (
        <form onSubmit={handleSubmit(onSubmit)}>
            <div className="absolute bottom-0 left-0 right-0 bg-white p-2 shadow-lg">
                <div className="flex items-center w-full gap-2">
                    <TextareaAutosize
                        className="bg-gray-100 w-full p-3 rounded-lg focus-visible:outline-none
                                    focus-visible:ring-0 shadow-none resize-none thin-scrollbar"
                        placeholder={t('writeYourMessage')}
                        minRows={1}
                        maxRows={5}
                        {...register("message", { required: true })}
                        disabled={isSubmitting}
                        onKeyDown={useEnterSubmit(handleSubmit(onSubmit))}
                    />

                    <Button
                        type="submit"
                        size="icon"
                        disabled={isSubmitting || message.trim() === ""}
                        className="min-w-8 min-h-8 flex items-center justify-center bg-black text-white rounded-full
                                    hover:bg-gray-500 transition-all">
                        <MoveUp className="h-4 w-4 stroke-2" />
                    </Button>
                </div>
            </div>
        </form>
    );
};