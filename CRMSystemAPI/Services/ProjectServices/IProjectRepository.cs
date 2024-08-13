using CRMSystemAPI.Models.DatabaseModels;

namespace CRMSystemAPI.Services.ProjectServices
{
    public interface IProjectRepository
    {
        Task<IEnumerable<Project>> GetProjectsByUserIdAsync(int userId);
        Task<IEnumerable<Project>> GetAllProjectsAsync();
        Task<Project?> GetProjectByIdAsync(int id);
        Task AddProjectAsync(Project project);
        Task UpdateProjectAsync(Project project);
        Task DeleteProjectAsync(Project project);
    }
}
