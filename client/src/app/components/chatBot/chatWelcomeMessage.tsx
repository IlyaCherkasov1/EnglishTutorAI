import {useTranslation} from "react-i18next";

interface Props {
    messagesLength: number;
}

export const ChatWelcomeMessage = (props: Props) => {
    const { t } = useTranslation();

    return (
        <>
            {props.messagesLength === 0 && (
                <div
                    className="flex justify-center items-center text-xl text-gray-500 flex-1">
                    {t('welcomeChatMessage')}
                </div>
            )}
        </>
    )
}