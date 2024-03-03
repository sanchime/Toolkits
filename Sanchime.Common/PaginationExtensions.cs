using Sanchime.Common.Models.Query;
using Sanchime.Common.Models;

namespace Sanchime.Common;

public static class PaginationExtensions
{
    public static PaginatedResult<TResponse> ToPageList<TResponse>(this IQueryable<TResponse> queryable, DynamicQuery query)
       where TResponse : class
    {
        var where = queryable.Where(query.Filters);
        return where.ToPageList(query.PageIndex, query.PageSize);
    }

    #region IQueryable

    public static PaginatedResult<TResponse> ToPageList<TResponse>(this IQueryable<TResponse> queryable, int page, int pageSize)
    {
        int count = queryable.Count();
        if (pageSize == -1)
        {
            return new([.. queryable], page, pageSize, count);
        }

        var items = queryable.Skip((page - 1) * pageSize).Take(pageSize).ToList();
        return new(items, count, page, pageSize);
    }

    public static PaginatedResult<TResponse> ToPageList<TResponse>(this IQueryable<TResponse> queryable, IPageableObject pageable)
    {
        int count = queryable.Count();
        if (pageable.PageSize == -1)
        {
            return new([.. queryable], pageable.PageIndex, pageable.PageSize, count);
        }

        var items = queryable.Skip((pageable.PageIndex - 1) * pageable.PageSize).Take(pageable.PageSize).ToList();
        return new(items, count, pageable.PageIndex, pageable.PageSize);
    }

    #endregion

    #region IEnumerable

    public static PaginatedResult<TResponse> ToPageList<TResponse>(this IEnumerable<TResponse> queryable, int page, int pageSize)
    {
        int count = queryable.Count();
        if (pageSize == -1)
        {
            return new([.. queryable], page, pageSize, count);
        }

        var items = queryable.Skip((page - 1) * pageSize).Take(pageSize).ToList();
        return new(items, count, page, pageSize);
    }

    public static PaginatedResult<TResponse> ToPageList<TResponse>(this IEnumerable<TResponse> queryable, IPageableObject pageable)
    {
        int count = queryable.Count();
        if (pageable.PageSize == -1)
        {
            return new([.. queryable], pageable.PageIndex, pageable.PageSize, count);
        }

        var items = queryable.Skip((pageable.PageIndex - 1) * pageable.PageSize).Take(pageable.PageSize).ToList();
        return new(items, count, pageable.PageIndex, pageable.PageSize);
    }

    #endregion

}
