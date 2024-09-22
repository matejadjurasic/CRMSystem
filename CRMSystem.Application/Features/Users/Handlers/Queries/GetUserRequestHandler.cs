using AutoMapper;
using CRMSystem.Application.Contracts.Persistence;
using CRMSystem.Application.DTOs.User;
using CRMSystem.Application.Exceptions;
using CRMSystem.Application.Features.Users.Requests.Queries;
using CRMSystem.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMSystem.Application.Features.Users.Handlers.Queries
{
    public class GetUserRequestHandler : IRequestHandler<GetUserRequest, UserReadDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public GetUserRequestHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<UserReadDto> Handle(GetUserRequest request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserByIdAsync(request.Id);
            return _mapper.Map<UserReadDto>(user);
        }
    }
}
