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
import {useTranslation} from "react-i18next";

interface Props {
    onConfirm: () => void;
}

export const DeleteTranslateModal = ({ onConfirm }: Props) => {
    const { t } = useTranslation();

    return (
        <Dialog>
            <DialogTrigger asChild>
                <div className="h-6 w-6 flex items-center justify-center text-gray-400
                     hover:text-red-500 transition-colors duration-300" aria-label="delete">
                    <Trash2 className="h-4 w-4 absolute top-2 right-2"/>
                </div>
            </DialogTrigger>
            <DialogContent className="sm:max-w-[425px]">
                <DialogHeader>
                    <DialogTitle>{t('deleteTranslate')}</DialogTitle>
                    <DialogDescription>{t('confirmDelete')}</DialogDescription>
                </DialogHeader>
                <div className="grid gap-4 py-4">
                    <p id="translate-name" className="text-gray-900 dark:text-gray-50"/>
                </div>
                <DialogFooter>
                    <DialogClose asChild>
                        <Button variant="destructive" onClick={onConfirm}>{t('delete')}</Button>
                    </DialogClose>
                    <DialogClose asChild>
                        <Button variant="outline">{t('cancel')}</Button>
                    </DialogClose>
                </DialogFooter>
            </DialogContent>
        </Dialog>
    )
}