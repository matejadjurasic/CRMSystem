using AutoMapper;
using CRMSystemAPI.Exceptions;
using CRMSystemAPI.Models.DatabaseModels;
using CRMSystemAPI.Models.DataTransferModels.ProjectTransferModels;
using System.Security.Claims;

namespace CRMSystemAPI.Services.ProjectServices
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<ProjectService> _logger;

        public ProjectService(IProjectRepository projectRepository, IMapper mapper, ILogger<ProjectService> logger)
        {
            _projectRepository = projectRepository;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<IEnumerable<ProjectReadDto>> GetUserProjectsAsync(int userId)
        {
            _logger.LogInformation("Attempting to retrieve projects for user ID: {UserId}", userId);
            var projects = await _projectRepository.GetProjectsByUserIdAsync(userId);

            if (projects == null || !projects.Any())
            {
                _logger.LogWarning("No projects found for user ID: {UserId}", userId);
                throw new NotFoundException("No projects found for the user.");
            }

            return _mapper.Map<IEnumerable<ProjectReadDto>>(projects);
        }

        public async Task<IEnumerable<ProjectReadDto>> GetAllProjectsAsync()
        {
            _logger.LogInformation("Retrieving all projects");
            var projects = await _projectRepository.GetAllProjectsAsync();
            return _mapper.Map<IEnumerable<ProjectReadDto>>(projects);
        }

        public async Task<ProjectReadDto> GetProjectByIdAsync(int id, int userId)
        {
            _logger.LogInformation("Retrieving project ID: {ProjectId} for user ID: {UserId}", id, userId);
            var project = await _projectRepository.GetProjectByIdAsync(id);

            if (project == null || project.UserId != userId)
            {
                _logger.LogWarning("Project with ID: {ProjectId} not found or access denied for user ID: {UserId}", id, userId);
                throw new NotFoundException($"Project with ID {id} not found or access denied.");
            }

            return _mapper.Map<ProjectReadDto>(project);
        }

        public async Task<ProjectReadDto> CreateProjectAsync(ProjectCreateDto createDto, int userId)
        {
            _logger.LogInformation("Creating new project for user ID: {UserId}", userId);
            var project = _mapper.Map<Project>(createDto);
            project.UserId = userId;

            await _projectRepository.AddProjectAsync(project);
            _logger.LogInformation("Project created with ID: {ProjectId}", project.Id);

            return _mapper.Map<ProjectReadDto>(project);
        }

        public async Task<ProjectReadDto> UpdateProjectAsync(int id, ProjectUpdateDto updateDto, int userId)
        {
            _logger.LogInformation("Updating project ID: {ProjectId} for user ID: {UserId}", id, userId);
            var existingProject = await _projectRepository.GetProjectByIdAsync(id);

            if (existingProject == null || existingProject.UserId != userId)
            {
                _logger.LogWarning("Project with ID: {ProjectId} not found or access denied for user ID: {UserId}", id, userId);
                throw new NotFoundException($"Project with ID {id} not found or access denied.");
            }

            _mapper.Map(updateDto, existingProject);
            await _projectRepository.UpdateProjectAsync(existingProject);
            _logger.LogInformation("Project with ID: {ProjectId} updated successfully", id);

            return _mapper.Map<ProjectReadDto>(existingProject);
        }

        public async Task<bool> DeleteProjectAsync(int id, int userId)
        {
            _logger.LogInformation("Deleting project ID: {ProjectId} for user ID: {UserId}", id, userId);
            var project = await _projectRepository.GetProjectByIdAsync(id);

            if (project == null || project.UserId != userId)
            {
                _logger.LogWarning("Project with ID: {ProjectId} not found or access denied for user ID: {UserId}", id, userId);
                throw new NotFoundException($"Project with ID {id} not found or access denied.");
            }

            await _projectRepository.DeleteProjectAsync(project);
            _logger.LogInformation("Project with ID: {ProjectId} deleted successfully", id);
            return true;
        }

    }
}
