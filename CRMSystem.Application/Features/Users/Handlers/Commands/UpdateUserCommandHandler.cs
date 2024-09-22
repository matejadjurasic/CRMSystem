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
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateUserCommandHandler> _logger;

        public UpdateUserCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<UpdateUserCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.UserRepository.GetAsync(request.Id);
            _mapper.Map(request.UserUpdateDto, user);
            user.NormalizedEmail = request.UserUpdateDto!.Email.ToUpper();

            try
            {
                _logger.LogDebug("Updating user with ID: {UserId}", request.Id);
                await _unitOfWork.UserRepository.UpdateAsync(user);
                await _unitOfWork.Save();
                _logger.LogInformation("User with ID: {UserId} updated successfully.", request.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating user with ID: {UserId}", request.Id);
                throw;
            }
        }
    }
}
