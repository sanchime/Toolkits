namespace Sanchime.Identity.WebApi.Requests;

public record struct UserByIdRequest(long UserId);

public record struct UserUpdateRequest(string UserName);