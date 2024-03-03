namespace Sanchime.Common.Models;

public sealed record PaginatedResult<TData>(IList<TData> Items, int PageIndex, int PageSize, int TotalCount)
{
    public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);
}