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
    public class GetUserProjectsRequestHandler : IRequestHandler<GetUserProjectsRequest, List<ProjectReadDto>>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IMapper _mapper;

        public GetUserProjectsRequestHandler(IProjectRepository projectRepository, IMapper mapper)
        {
            _projectRepository = projectRepository;
            _mapper = mapper;
        }

        public async Task<List<ProjectReadDto>> Handle(GetUserProjectsRequest request, CancellationToken cancellationToken)
        {
            var projects = await _projectRepository.GetProjectsByUserIdAsync(request.UserId);
            return _mapper.Map<List<ProjectReadDto>>(projects);
        }
    }
}
