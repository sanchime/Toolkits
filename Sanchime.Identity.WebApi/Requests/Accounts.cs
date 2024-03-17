namespace Sanchime.Identity.WebApi.Requests;

public record struct AccountByIdRequest(long AccountId);


public record struct AccountChangePasswordRequest(string Password, string NewPassword);