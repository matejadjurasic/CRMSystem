﻿using AutoMapper;
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

        public ProjectService(IProjectRepository projectRepository, IMapper mapper)
        {
            _projectRepository = projectRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<ProjectReadDto>> GetUserProjectsAsync(int userId)
        {
            var projects = await _projectRepository.GetProjectsByUserIdAsync(userId);

            return projects == null
                ? throw new NotFoundException("No projects found for the user.")
                : _mapper.Map<IEnumerable<ProjectReadDto>>(projects);
        }

        public async Task<IEnumerable<ProjectReadDto>> GetAllProjectsAsync()
        {
            var projects = await _projectRepository.GetAllProjectsAsync();
            return _mapper.Map<IEnumerable<ProjectReadDto>>(projects);
        }

        public async Task<ProjectReadDto> GetProjectByIdAsync(int id, int userId)
        {
            var project = await _projectRepository.GetProjectByIdAsync(id);

            if (project == null || project.UserId != userId)
                throw new NotFoundException($"Project with ID {id} not found or access denied.");

            return _mapper.Map<ProjectReadDto>(project);
        }

        public async Task<ProjectReadDto> CreateProjectAsync(ProjectCreateDto createDto, int userId)
        {
            var project = _mapper.Map<Project>(createDto);
            project.UserId = userId;

            await _projectRepository.AddProjectAsync(project);

            return _mapper.Map<ProjectReadDto>(project);
        }

        public async Task<ProjectReadDto> UpdateProjectAsync(int id, ProjectUpdateDto updateDto, int userId)
        {
            var existingProject = await _projectRepository.GetProjectByIdAsync(id);

            if (existingProject == null || existingProject.UserId != userId)
                throw new NotFoundException($"Project with ID {id} not found or access denied.");

            _mapper.Map(updateDto, existingProject);
            await _projectRepository.UpdateProjectAsync(existingProject);

            return _mapper.Map<ProjectReadDto>(existingProject);
        }

        public async Task<bool> DeleteProjectAsync(int id, int userId)
        {
            var project = await _projectRepository.GetProjectByIdAsync(id);

            if (project == null || project.UserId != userId)
                throw new NotFoundException($"Project with ID {id} not found or access denied.");

            await _projectRepository.DeleteProjectAsync(project);
            return true;
        }

    }
}
