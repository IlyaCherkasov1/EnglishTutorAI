import {contextStore} from "@/app/infrastructure/stores/contextStore.ts";

export const UserMainInfo = () => {
    return (
        <div>
            <div className="flex items-center">
                <div
                    className="w-16 h-16 rounded-full bg-blue-100 flex items-center
                    justify-center text-2xl font-semibold text-blue-600">
                    {contextStore.firstName!.charAt(0).toUpperCase()}
                </div>
                <div className="ml-4">
                    <h1 className="text-2xl font-bold text-gray-800">
                        {contextStore.firstName}
                    </h1>
                    <p className="text-sm text-gray-500">
                        {contextStore.email}
                    </p>
                </div>
            </div>
        </div>
    )
}