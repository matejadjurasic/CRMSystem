using AutoMapper;
using CRMSystem.Application.DTOs.User;
using CRMSystem.Domain.Models;

namespace CRMSystem.Application.Profiles
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
                    : new List<string>()))
                .ReverseMap();
            CreateMap<User, UserCreateDto>().ReverseMap();
            CreateMap<User, UserUpdateDto>().ReverseMap();
        }
    }
}
