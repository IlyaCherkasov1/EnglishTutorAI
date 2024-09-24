import {Trash2} from "lucide-react";
import {
    Dialog,
    DialogClose,
    DialogContent,
    DialogDescription,
    DialogFooter,
    DialogHeader,
    DialogTitle,
    DialogTrigger
} from "@/app/components/ui/dialog.tsx";
import {Button} from "@/app/components/ui/button.tsx";

interface Props {
    onConfirm: () => void;
}

export const DeleteDocumentModal = ({ onConfirm }: Props) => {
    return (
        <Dialog>
            <DialogTrigger asChild>
                <div className="h-6 w-6 flex items-center justify-center text-gray-400
                     hover:text-red-500 transition-colors duration-300" aria-label="delete">
                    <Trash2 className="h-4 w-4"/>
                </div>
            </DialogTrigger>
            <DialogContent className="sm:max-w-[425px]">
                <DialogHeader>
                    <DialogTitle>Удалить документ</DialogTitle>
                    <DialogDescription>Вы уверены, что хотите удалить этот документ?</DialogDescription>
                </DialogHeader>
                <div className="grid gap-4 py-4">
                    <p id="document-name" className="text-gray-900 dark:text-gray-50"/>
                </div>
                <DialogFooter>
                    <DialogClose asChild>
                        <Button variant="destructive" onClick={onConfirm}>Удалить</Button>
                    </DialogClose>
                    <DialogClose asChild>
                        <Button variant="outline">Отмена</Button>
                    </DialogClose>
                </DialogFooter>
            </DialogContent>
        </Dialog>
    )
}