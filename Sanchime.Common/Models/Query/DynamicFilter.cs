namespace Sanchime.Common.Models.Query;

public sealed class DynamicFilter
{
    public required string PropertyName { get; set; }
    public DynamicOperationMode Operation { get; set; }
    public object? Value { get; set; }
    public DynamicConditionKind Condition { get; set; }
}
