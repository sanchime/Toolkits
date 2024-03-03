namespace Sanchime.Identity.WebApi.Requests;

public record UserByIdRequest(long UserId);

public record UserUpdateRequest(string UserName);