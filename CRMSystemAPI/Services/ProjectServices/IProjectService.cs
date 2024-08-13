using CRMSystemAPI.Models.DataTransferModels.ProjectTransferModels;

namespace CRMSystemAPI.Services.ProjectServices
{
    public interface IProjectService
    {
        Task<IEnumerable<ProjectReadDto>> GetUserProjectsAsync(int userId);
        Task<IEnumerable<ProjectReadDto>> GetAllProjectsAsync();
        Task<ProjectReadDto?> GetProjectByIdAsync(int id, int userId);
        Task<ProjectReadDto> CreateProjectAsync(ProjectCreateDto createDto, int userId);
        Task<ProjectReadDto?> UpdateProjectAsync(int id, ProjectUpdateDto updateDto, int userId);
        Task<bool> DeleteProjectAsync(int id, int userId);
    }
}
