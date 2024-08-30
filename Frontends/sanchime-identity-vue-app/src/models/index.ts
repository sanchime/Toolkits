export interface PaginationRequest {
    pageIndex: number
    pageSize: number
}
// IList<TData> Items, int PageIndex, int PageSize, int TotalCount
export interface PaginatedResult<T> {
    items: T[],
    pageIndex: number,
    pageSize: number,
    totalCount: number
}

export * from './account'
export * from './user'
export * from './menu'
export * from './role'
export * from './permission'