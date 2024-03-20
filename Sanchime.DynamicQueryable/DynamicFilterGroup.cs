namespace Sanchime.DynamicQueryable;

public sealed class DynamicFilterGroup
{
    public IList<DynamicFilter> Children { get; set; } = [];

    public DynamicConditionKind Condition { get; set; }
}
