import {cn} from "@/lib/utils.ts";

interface Props {
    className?: string;
}

export const SubmitSpinner = (props: Props) => {
    return (
        <div className="flex gap-4 p-4 flex-wrap justify-center">
            <img className={cn("w-5 h-5 animate-spin", props.className)} src="/spinners/submitSpinner.svg"
                 alt="Loading icon" />
        </div>
    )
};