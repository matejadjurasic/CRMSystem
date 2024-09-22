using AutoMapper;
using CRMSystem.Application.Contracts.Persistence;
using CRMSystem.Application.DTOs.Project;
using CRMSystem.Application.Exceptions;
using CRMSystem.Application.Features.Projects.Requests.Queries;
using CRMSystem.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMSystem.Application.Features.Projects.Handlers.Queries
{
    public class GetProjectRequestHandler : IRequestHandler<GetProjectRequest, ProjectReadDto>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IMapper _mapper;

        public GetProjectRequestHandler(IProjectRepository projectRepository, IMapper mapper)
        {
            _projectRepository = projectRepository;
            _mapper = mapper;
        }
        public async Task<ProjectReadDto> Handle(GetProjectRequest request, CancellationToken cancellationToken)
        {
            var project = await _projectRepository.GetAsync(request.Id);

            if (project.UserId != request.UserId)
                throw new InvalidOperationException("Project doesn't belong to the logged in user");

            return _mapper.Map<ProjectReadDto>(project);
        }
    }
}
