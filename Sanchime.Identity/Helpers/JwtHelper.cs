using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Text;

namespace Sanchime.Identity.Helpers;

public static class JwtHelper
{
    private static IEnumerable<Claim> GetClaims(this Account account)
    {
        var userClaims = new List<Claim>
        {
            new("account", account.LoginName),
            new("userId", account.User.Id.ToString()),
            new("userName", account.User.Name),
        };

        return [.. userClaims];
    }

    private static SigningCredentials GetSigningCredentials(string secret)
    {
        var bytes = Encoding.UTF8.GetBytes(secret);
        return new SigningCredentials(new SymmetricSecurityKey(bytes), SecurityAlgorithms.HmacSha256);
    }

    public static string GenerateRefreshToken()
    {
        var randomNumber = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }

    public static string GenerateJwtToken(Account account, IdentityConfiguration config)
    {
        return GetSigningCredentials(config.Secret).GenerateEncryptedToken(account.GetClaims(), config.Expiration);
    }

    public static string GenerateEncryptedToken(this SigningCredentials signingCredentials, IEnumerable<Claim> claims, TimeSpan expires)
    {
        var token = new JwtSecurityToken(
           claims: claims,
           expires: DateTime.UtcNow.Add(expires),
           signingCredentials: signingCredentials);
        var tokenHandler = new JwtSecurityTokenHandler();
        var encryptedToken = tokenHandler.WriteToken(token);
        return encryptedToken;
    }

    public static ClaimsPrincipal GetPrincipalFromExpiredToken(IdentityConfiguration config, string token, string securityTokenExceptionMessage)
    {
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.Secret)),
            ValidateIssuer = false,
            ValidateAudience = false,
            RoleClaimType = ClaimTypes.Role,
            ClockSkew = TimeSpan.Zero
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var securityToken);
        if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
            StringComparison.InvariantCultureIgnoreCase))
        {
            throw new SecurityTokenException(securityTokenExceptionMessage);
        }

        return principal;
    }

}
