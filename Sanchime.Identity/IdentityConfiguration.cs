namespace Sanchime.Identity;

/// <summary>
/// 登录认证配置
/// </summary>
public class IdentityConfiguration
{
    public required string Secret { get; set; }

    public TimeSpan Expiration { get; set; }

    public string DefaultPassword { get; set; } = "123456";

    public string DefaultAdministratorAccount { get; set; } = "admin";

    public string DefaultAdministratorPassword { get; set; } = "admin";

    public string DefaultRoleCode { get; set; } = "Administrator";

    public string DefaultRoleName { get; set; } = "管理员";
}
