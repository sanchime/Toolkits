namespace Sanchime.Identity.Responses;

public record LoginResponse(string Token, DateTimeOffset RefreshTokenExpiryTime, string? RefreshToken = null);
