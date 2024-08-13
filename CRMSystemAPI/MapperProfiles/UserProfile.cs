using AutoMapper;
using CRMSystemAPI.Models.DatabaseModels;
using CRMSystemAPI.Models.DataTransferModels.UserTransferModels;

namespace CRMSystemAPI.MapperProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserReadDto>()
                //.ForMember(dest => dest.Roles, opt => opt.MapFrom(src => src.UserRoles.Select(ur => ur.Role.Name).ToList()));
            .ForMember(dest => dest.Roles, opt => opt.MapFrom(src =>
            src.UserRoles != null
            ? src.UserRoles.Select(ur => ur.Role != null ? ur.Role.Name : string.Empty).ToList()
            : new List<string>()));
            CreateMap<UserCreateDto, User>();
            CreateMap<UserUpdateDto, User>();
        }
    }
}
