using AutoMapper;
using CRMSystem.Application.DTOs.Project;
using CRMSystem.Domain.Models;

namespace CRMSystem.Application.Profiles
{
    public class ProjectProfile : Profile
    {
        public ProjectProfile()
        {
            CreateMap<Project, ProjectReadDto>().ReverseMap();
            CreateMap<Project, ProjectCreateDto>().ReverseMap();
            CreateMap<Project, ProjectUpdateDto>().ReverseMap();
        }
    }
}
