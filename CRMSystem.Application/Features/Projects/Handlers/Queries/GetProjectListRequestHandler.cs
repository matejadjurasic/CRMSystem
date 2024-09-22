using AutoMapper;
using CRMSystem.Application.Contracts.Persistence;
using CRMSystem.Application.DTOs.Project;
using CRMSystem.Application.Features.Projects.Requests.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMSystem.Application.Features.Projects.Handlers.Queries
{
    public class GetProjectListRequestHandler : IRequestHandler<GetProjectListRequest, List<ProjectReadDto>>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IMapper _mapper;

        public GetProjectListRequestHandler(IProjectRepository projectRepository, IMapper mapper)
        {
            _projectRepository = projectRepository;
            _mapper = mapper;
        }

        public async Task<List<ProjectReadDto>> Handle(GetProjectListRequest request, CancellationToken cancellationToken)
        {
            var projects = await _projectRepository.GetAllAsync();
            return _mapper.Map<List<ProjectReadDto>>(projects); 
        }
    }
}
