using AutoMapper;
using CRMSystem.Application.Contracts.Persistence;
using CRMSystem.Application.Exceptions;
using CRMSystem.Application.Features.Users.Requests.Commands;
using CRMSystem.Domain.Models;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMSystem.Application.Features.Users.Handlers.Commands
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<DeleteUserCommandHandler> _logger;

        public DeleteUserCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<DeleteUserCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Attempting to delete user with ID: {UserId}", request.Id);
                var user = await _unitOfWork.UserRepository.GetAsync(request.Id);
                await _unitOfWork.UserRepository.DeleteAsync(user);
                await _unitOfWork.Save();
                _logger.LogInformation("User with ID: {UserId} was deleted successfully.", request.Id);
            }catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting user with ID: {UserId}", request.Id);
                throw;
            }
        }
    }
}
