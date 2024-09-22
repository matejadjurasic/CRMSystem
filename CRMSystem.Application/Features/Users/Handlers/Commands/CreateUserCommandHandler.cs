using AutoMapper;
using CRMSystem.Application.Contracts.Infrastructure;
using CRMSystem.Application.Contracts.Persistence;
using CRMSystem.Application.Exceptions;
using CRMSystem.Application.Features.Users.Requests.Commands;
using CRMSystem.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMSystem.Application.Features.Users.Handlers.Commands
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailSender _emailSender;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateUserCommandHandler> _logger;

        public CreateUserCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IEmailSender emailSender, UserManager<User> userManager, ILogger<CreateUserCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _emailSender = emailSender;
            _userManager = userManager;
            _logger = logger;
        }

        public async Task<int> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Attempting to create a new user with email:{Email}", request.UserCreateDto!.Email);

            var user = new User
            {
                Email = request.UserCreateDto!.Email,
                Name = request.UserCreateDto.Name,
                UserName = request.UserCreateDto.Email,
                NormalizedUserName = request.UserCreateDto.Email.ToUpper(),
                NormalizedEmail = request.UserCreateDto.Email.ToUpper(),
                SecurityStamp = Guid.NewGuid().ToString("D")
            };

            var password = $"{user.Name}123";
            user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, password);

            try
            {
                _logger.LogDebug("Adding user to the database...");
                await _unitOfWork.UserRepository.AddAsync(user);
                await _unitOfWork.Save();

                _logger.LogInformation("User with email {Email} created successfully.", user.Email);

                _logger.LogDebug("Assigning 'Client' role to the user with email: {Email}", user.Email);
                await _unitOfWork.UserRepository.AddUserToClientRole(user);
                await _unitOfWork.Save();

                _logger.LogDebug("Sending welcome email to {Email}", user.Email);
                await _emailSender.SendWelcomeEmailAsync(user, password);
                _logger.LogInformation("Welcome email to {Email} sent successfully.", user.Email);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Failed to create user with email {Email} due to an existing user with the same email.", user.Email);
                throw new DuplicateEmailException("A user with this email already exists.");
            }catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while creating the user with email {Email}", user.Email);
                throw;
            }

            return user.Id;
        }
    }
}
