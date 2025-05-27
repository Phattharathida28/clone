
export interface SortEvent {
    data?: any[];
    mode?: string;
    field?: string;
    order?: number;
    multiSortMeta?: SortMeta[];
}

export interface SortMeta {
    field: string;
    order: number;
}
export interface PageEvent {
    first: number;
    rows: number;
    page: number;
    pageCount: number;
}

export interface PaginatorState {
    page?: number;
    first?: number;
    rows?: number;
    pageCount?: number;
}