namespace Sanchime.Identity.Mappings;

internal class PermissionProfile : Profile
{
    public PermissionProfile()
    {
        CreateMap<Permission, PermissionResponse>();
    }
}
