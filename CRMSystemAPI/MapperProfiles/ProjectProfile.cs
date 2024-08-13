using AutoMapper;
using CRMSystemAPI.Models.DatabaseModels;
using CRMSystemAPI.Models.DataTransferModels.ProjectTransferModels;

namespace CRMSystemAPI.MapperProfiles
{
    public class ProjectProfile : Profile
    {
        public ProjectProfile()
        {
            CreateMap<Project, ProjectReadDto>();
            CreateMap<ProjectCreateDto, Project>();
            CreateMap<ProjectUpdateDto, Project>();
        }
    }
}
