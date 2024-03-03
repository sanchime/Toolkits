﻿namespace Sanchime.Common.Models.Query;

public class DynamicQuery : IPageableObject
{
    public int PageIndex { get; set; } = 1;

    public int PageSize { get; set; } = 30;

    public IList<DynamicFilterGroup> Filters { get; set; } = [];
}
