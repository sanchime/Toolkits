using Sanchime.Common.Models;

namespace Sanchime.DynamicQueryable;

public class DynamicQuery : IPageableObject
{
    public int PageIndex { get; set; } = 1;

    public int PageSize { get; set; } = 30;

    public IList<DynamicFilterGroup> Filters { get; set; } = [];
}
