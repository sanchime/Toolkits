using Microsoft.EntityFrameworkCore;
using Sanchime.Common.Models;

namespace Sanchime.EntityFrameworkCore;

public static class TreeExtensions
{
    public static async ValueTask<List<T>> ToTreeAsync<T, TKey>(this IQueryable<T> query, TKey rootValue, Func<T, TKey> keySelector, Func<T, TKey?> parentSelector, CancellationToken cancellation = default)
        where T : ITree<T>
    {
        var list = await query.ToArrayAsync(cancellation);
        if (list.Length == 0)
        {
            return [];
        }

        var lookup = list.ToLookup(parentSelector);
        var roots = lookup[rootValue].ToList();

        foreach (var root in roots)
        {
            root.Children = BuildTree(root).ToList();
        }

        return roots;

        IEnumerable<T> BuildTree(T node)
        {
            foreach (var child in lookup[keySelector(node)])
            {
                child.Children = BuildTree(child).ToList();
                yield return child;
            }
        }
    }
}
