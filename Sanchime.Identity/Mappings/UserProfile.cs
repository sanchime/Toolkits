namespace Sanchime.Identity.Mappings;

internal class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserResponse>();

        CreateMap<AddUserCommand, User>();

        CreateMap<User, UserRolesResponse>();
    }
}
