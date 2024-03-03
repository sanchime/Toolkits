using System.Linq.Expressions;

namespace Sanchime.EntityFrameworkCore;

public static class WhereExtensions
{
    public static IQueryable<TEntity> Where<TEntity>(this IQueryable<TEntity> source, bool condition, Expression<Func<TEntity, bool>> predicate) 
        => condition ? source.Where(predicate) : source;
}
