namespace Sanchime.Identity.WebApi.Requests;

public record struct UserUpdateRequest(string UserName, bool IsEnabled);