namespace Sanchime.Common.Models;

public record struct PaginatedResult<TData>(IList<TData> Items, int PageIndex, int PageSize, int TotalCount)
{
    public readonly int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);
}