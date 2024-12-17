import { X } from "lucide-react";
import { Button } from "@/app/components/ui/button.tsx";

interface Props {
    closeChat: () => void;
}

export const ChatHeader = ({ closeChat }: Props) => (
    <div className="absolute top-2 right-2">
        <Button onClick={closeChat} className="bg-white hover:bg-gray-100">
            <X className="h-5 w-5 text-gray-400" />
        </Button>
    </div>
);