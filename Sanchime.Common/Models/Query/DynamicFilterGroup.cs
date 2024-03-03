namespace Sanchime.Common.Models.Query;

public sealed class DynamicFilterGroup
{
    public IList<DynamicFilter> Children { get; set; } = [];

    public DynamicConditionKind Condition { get; set; }
}
