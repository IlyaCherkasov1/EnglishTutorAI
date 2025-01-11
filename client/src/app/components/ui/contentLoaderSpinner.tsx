import {cn} from "@/lib/utils.ts";

interface Props {
    className?: string;
}

export const ContentLoaderSpinner = (props: Props) => {
    return (
        <div className="flex gap-4 p-4 flex-wrap justify-center">
            <img className={cn("w-12 h-12 animate-spin", props.className)} src="/spinners/contentLoaderSpinner.svg"
                 alt="Loading icon" />
        </div>
    );
};