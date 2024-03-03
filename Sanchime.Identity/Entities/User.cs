namespace Sanchime.Identity.Entities;

public class User : IdentityEntity
{
    /// <summary>
    /// 用户名
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// 用户邮箱
    /// </summary>
    public string? Email { get; set; }

    /// <summary>
    /// 用户电话
    /// </summary>
    public string? Phone { get; set; }

    /// <summary>
    /// 头像
    /// </summary>
    public string? Avatar { get; set; }

    public long AccountId { get; set; }

    public Account Account { get; set; } = null!;

    public UserGroup? UserGroup { get; set; }

    /// <summary>
    /// 角色
    /// </summary>
    public HashSet<Role> Roles { get; set; } = [];
}
