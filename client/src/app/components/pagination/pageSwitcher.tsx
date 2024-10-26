import {
    Pagination,
    PaginationContent,
    PaginationLink,
    PaginationNext,
    PaginationPrevious
} from "../ui/pagination"
import {PaginationItem} from "@/app/components/ui/pagination.tsx";

interface Props {
    currentPage: number;
    totalPages: number;
    onPageChange: (page: number) => void;
}

export const PageSwitcher = (props: Props) => {
    const goToPreviousPage = () => {
        if (props.currentPage > 1) {
            props.onPageChange(props.currentPage - 1);
        }
    };

    const goToNextPage = () => {
        if (props.currentPage < props.totalPages){
            props.onPageChange(props.currentPage + 1);
        }
    };

    return(
        <Pagination>
            <PaginationContent>
                <PaginationItem>
                    <PaginationPrevious onClick={goToPreviousPage} />
                </PaginationItem>
                <PaginationItem>
                    <PaginationLink>{props.currentPage} / {props.totalPages}</PaginationLink>
                </PaginationItem>
                <PaginationItem>
                    <PaginationNext onClick={goToNextPage} />
                </PaginationItem>
            </PaginationContent>
        </Pagination>
    )
}