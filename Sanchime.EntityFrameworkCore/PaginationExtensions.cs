using Sanchime.Common.Models.Query;
using Sanchime.Common.Models;
using Microsoft.EntityFrameworkCore;
using Sanchime.Common;

namespace Sanchime.EntityFrameworkCore;

public static class PaginationExtensions
{
    public static ValueTask<PaginatedResult<TResponse>> ToPageListAsync<TResponse>(this IQueryable<TResponse> queryable, DynamicQuery query)
       where TResponse : class
    {
        var where = queryable.Where(query.Filters);
        return where.ToPageListAsync(query.PageIndex, query.PageSize);
    }

    public static async ValueTask<PaginatedResult<TResponse>> ToPageListAsync<TResponse>(this IQueryable<TResponse> queryable, int page, int pageSize)
    {
        int count = await queryable.CountAsync();
        if (pageSize == -1)
        {
            return new([.. queryable], page, pageSize, count);
        }

        var items = await queryable.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
        return new(items, count, page, pageSize);
    }
}
