import {ReactNode, useEffect, useRef} from "react";

interface InfiniteScrollProps {
    loadMore: () => void;
    hasMore: boolean;
    loader: ReactNode;
    children: ReactNode;
}

export const InfiniteScroll: React.FC<InfiniteScrollProps> = ({ loadMore, hasMore, loader, children }) => {
    const observerRef = useRef<HTMLDivElement | null>(null);

    useEffect(() => {
        const observer = new IntersectionObserver(
            (entries) => {
                if (entries[0].isIntersecting && hasMore) {
                    loadMore();
                }
            },
            { threshold: 0.7 }
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
    }, [loadMore, hasMore]);

    return (
        <div>
            {children}
            {hasMore && (
                <div ref={observerRef} className="flex justify-center items-center mt-4 h-10">
                    {loader}
                </div>
            )}
        </div>
    );
};