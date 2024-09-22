using AutoMapper;
using CRMSystem.Application.Contracts.Persistence;
using CRMSystem.Application.DTOs.User;
using CRMSystem.Application.Features.Projects.Requests.Queries;
using CRMSystem.Application.Features.Users.Requests.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMSystem.Application.Features.Users.Handlers.Queries
{
    public class GetUserListRequestHandler : IRequestHandler<GetUserListRequest, List<UserReadDto>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public GetUserListRequestHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<List<UserReadDto>> Handle(GetUserListRequest request, CancellationToken cancellationToken)
        {
            var projects = await _userRepository.GetAllUsersAsync();
            return _mapper.Map<List<UserReadDto>>(projects);
        }
    }
}
