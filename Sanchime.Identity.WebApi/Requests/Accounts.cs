namespace Sanchime.Identity.WebApi.Requests;

public record struct AccountChangePasswordRequest(string Password, string NewPassword);