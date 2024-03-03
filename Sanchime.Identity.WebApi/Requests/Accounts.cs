namespace Sanchime.Identity.WebApi.Requests;

public record AccountByIdRequest(long AccountId);


public record AccountChangePasswordRequest(string Password, string NewPassword);