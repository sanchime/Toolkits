namespace Sanchime.Identity.Responses;


public record UserResponse(long Id, string Name, string? Phone, string? Email, string? Avatar, bool IsEnabled);


public record UserRolesResponse(long Id, string Name, RoleResponse[] Roles);