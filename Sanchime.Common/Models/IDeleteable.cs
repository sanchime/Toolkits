namespace Sanchime.Common.Models;

/// <summary>
/// 软删除
/// </summary>
public interface IDeleteable
{
    bool IsDeleted { get; set; }
}
