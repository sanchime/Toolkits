namespace Sanchime.Identity.Entities;

public class Account : IdentityEntity
{
    public required string LoginName { get; set; }

    public required string PasswordHash { get; set; }

    public string? RefreshToken { get; set; }

    public DateTimeOffset? RefreshTokenExpiryTime { get; set; }

    public User User { get; set; } = null!;
}
