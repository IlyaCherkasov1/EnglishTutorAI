import {TranslateListItemComponent} from "@/app/components/translate/translateListItemComponent.tsx";
import {TranslateListItem} from "@/app/dataModels/translate/translateListItem.ts";

interface Props {
    allTranslates: TranslateListItem[];
    onDelete(translateId: string): void;
}

export const TranslatesList = (props: Props) => {
    if (!props.allTranslates || props.allTranslates.length === 0) {
        return null;
    }

    return (
        <main>
            <div className="flex justify-center items-center">
                <div className="grid gap-8 p-4 grid-cols-1 grid-md:grid-cols-2 grid-lg:grid-cols-3">
                    {props.allTranslates.map((translate, index) =>
                        <TranslateListItemComponent
                            key={translate.id}
                            translate={translate}
                            onDelete={props.onDelete}
                            index={index}
                        />)}
                </div>
            </div>
        </main>
    );
};