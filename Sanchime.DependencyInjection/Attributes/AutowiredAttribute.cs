namespace Sanchime.DependencyInjection;

/// <summary>
/// 字段或属性注入
/// </summary>
[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
public class AutowiredAttribute : Attribute, IInjectable
{
    public string? ServiceKey { get; }
}
