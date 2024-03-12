namespace Sanchime.Identity.Mappings;

internal class RoleProfile : Profile
{
    public RoleProfile()
    {
        CreateMap<Role, RoleResponse>();
    }
}
