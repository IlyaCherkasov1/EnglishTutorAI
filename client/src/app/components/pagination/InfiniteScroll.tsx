import {ReactNode, useEffect, useRef, useState} from "react";

interface Props {
    loadMore: () => Promise<void>;
    hasMore: boolean;
    loader: ReactNode;
    children: ReactNode;
    minLoaderTime?: number;
}

export const InfiniteScroll = ({ loadMore, hasMore, loader, children, minLoaderTime = 1000 }: Props) => {
    const observerRef = useRef<HTMLDivElement | null>(null);
    const [isLoading, setIsLoading] = useState(false);

    useEffect(() => {
        const observer = new IntersectionObserver(
            async (entries) => {
                if (entries[0].isIntersecting && hasMore && !isLoading) {
                    setIsLoading(true);

                    const startTime = Date.now();

                    await loadMore();

                    const elapsedTime = Date.now() - startTime;
                    if (elapsedTime < minLoaderTime) {
                        await new Promise((resolve) => setTimeout(resolve, minLoaderTime - elapsedTime));
                    }

                    setIsLoading(false);
                }
            },
            { threshold: 0.9 }
        );

        const currentObserverRef = observerRef.current;

        if (currentObserverRef) {
            observer.observe(currentObserverRef);
        }

        return () => {
            if (currentObserverRef) {
                observer.unobserve(currentObserverRef);
            }
        };
    }, [loadMore, hasMore, isLoading, minLoaderTime]);

    return (
        <div>
            {children}
            {hasMore  && (
                <div ref={observerRef} className="flex justify-center items-center mt-4 h-10">
                    {isLoading && loader}
                </div>
            )}
        </div>
    );
};