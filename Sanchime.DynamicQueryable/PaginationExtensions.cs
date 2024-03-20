using Sanchime.Common.Models;
using Sanchime.Common;


namespace Sanchime.DynamicQueryable;

public static class PaginationExtensions
{
    public static PaginatedResult<TResponse> ToPageList<TResponse>(this IQueryable<TResponse> queryable, DynamicQuery query)
       where TResponse : class
    {
        var where = queryable.Where(query.Filters);
        return where.ToPageList(query.PageIndex, query.PageSize);
    }
}
