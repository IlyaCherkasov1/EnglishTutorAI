import { useState } from "react";

export function useInfiniteScroll<T>(
    fetchItems: (page: number, pageSize: number) => Promise<{ items: T[]; totalCount: number }>,
    pageSize: number
) {
    const [items, setItems] = useState<T[]>([]);
    const [page, setPage] = useState(1);
    const [totalCount, setTotalCount] = useState<number | null>(null);
    const [hasMore, setHasMore] = useState(true);
    const [isInitialLoad, setIsInitialLoad] = useState(true);

    const loadMoreItems = async () => {
        const response = await fetchItems(page, pageSize);

        if (isInitialLoad) {
            setIsInitialLoad(false);
        }

        if (totalCount === null) {
            setTotalCount(response.totalCount);
        }

        setItems((prev) => [...prev, ...response.items]);
        const totalLoaded = items.length + response.items.length;
        setHasMore(totalLoaded < (response.totalCount || 0));

        if (totalLoaded < (response.totalCount || 0)) {
            setPage((prev) => prev + 1);
        }
    };

    return {
        items,
        hasMore,
        isInitialLoad,
        loadMoreItems,
        totalCount,
    };
}