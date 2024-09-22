using AutoMapper;
using CRMSystem.Application.Contracts.Persistence;
using CRMSystem.Application.Exceptions;
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
    public class UpdateProjectCommandHandler : IRequestHandler<UpdateProjectCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateProjectCommandHandler> _logger;

        public UpdateProjectCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<UpdateProjectCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
        {
            var project = await _unitOfWork.ProjectRepository.GetAsync(request.Id);

            if (project.UserId != request.UserId)
            {
                _logger.LogWarning("UserId: {UserId} attempted to delete project with ID: {ProjectId} that doesn't belong to them", request.UserId, request.Id);
                throw new InvalidOperationException("Project doesn't belong to the logged in user");
            }
      
            _mapper.Map(request.ProjectUpdateDto, project);

            try
            {
                _logger.LogDebug("Updating project with ID: {ProjectId}", request.Id);
                await _unitOfWork.ProjectRepository.UpdateAsync(project);
                await _unitOfWork.Save();
                _logger.LogInformation("Successfully updated project with ID: {ProjectId} for UserId: {UserId}", request.Id, request.UserId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating project with ID: {ProjectId} for UserId: {UserId}", request.Id, request.UserId);
                throw;
            }
        }
    }
}
