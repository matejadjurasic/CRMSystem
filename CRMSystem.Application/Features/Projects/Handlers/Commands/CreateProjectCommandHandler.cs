using AutoMapper;
using CRMSystem.Application.Contracts.Persistence;
using CRMSystem.Application.Features.Projects.Requests.Commands;
using CRMSystem.Domain.Models;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMSystem.Application.Features.Projects.Handlers.Commands
{
    public class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand, int>
    { 
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateProjectCommandHandler> _logger;

        public CreateProjectCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<CreateProjectCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<int> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
        {
            var project = _mapper.Map<Project>(request.ProjectCreateDto);
            project.UserId = request.UserId;

            try
            {
                _logger.LogDebug("Attempting to add a new project for UserId: {UserId}", request.UserId);
                project = await _unitOfWork.ProjectRepository.AddAsync(project);
                await _unitOfWork.Save();
                _logger.LogInformation("Successfully created a new project with ID: {ProjectId} for UserId: {UserId}", project.Id, request.UserId);

                return project.Id;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating a project for UserId: {UserId}", request.UserId);
                throw;
            }
        }
    }
}
