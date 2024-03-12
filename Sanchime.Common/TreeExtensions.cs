using Sanchime.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sanchime.Common;

public static class TreeExtensions
{
    public static List<T> ToTree<T, TKey>(this IEnumerable<T> list, TKey rootValue, Func<T, TKey> keySelector, Func<T, TKey?> parentSelector)
        where T : ITree<T>
    {
        if (!list.Any())
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
