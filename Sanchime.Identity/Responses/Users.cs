namespace Sanchime.Identity.Responses;


public record UserResponse(long Id, string Name, string? Phone, string? Email, string? Avatar);


public record UserRolesResponse(long Id, string Name, RoleResponse[] Roles);