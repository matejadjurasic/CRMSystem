using AutoMapper;
using CRMSystemAPI.Exceptions;
using CRMSystemAPI.Models.DatabaseModels;
using CRMSystemAPI.Models.DataTransferModels.UserTransferModels;
using CRMSystemAPI.Services.AuthServices;
using CRMSystemAPI.Services.EmailServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace CRMSystemAPI.Services.UserServices
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly IEmailSender _emailSender;
        private readonly ILogger<UserService> _logger;

        public UserService(IUserRepository userRepository, IMapper mapper, UserManager<User> userManager, IEmailSender emailSender, ILogger<UserService> logger)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _userManager = userManager;
            _emailSender = emailSender;
            _logger = logger;
        }

        public async Task<IEnumerable<UserReadDto>> GetUsersAsync()
        {
            _logger.LogInformation("Fetching all users");
            var users = await _userRepository.GetAllUsersAsync();
            return _mapper.Map<IEnumerable<UserReadDto>>(users);
        }

        public async Task<UserReadDto> GetUserByIdAsync(int id)
        {
            _logger.LogInformation("Fetching user by ID: {UserId}", id);
            var user = await _userRepository.GetUserByIdAsync(id);
            if (user == null) 
            {
                _logger.LogWarning("User not found with ID: {UserId}", id);
                throw new NotFoundException("User not found");
            }
            return _mapper.Map<UserReadDto>(user);
        }

        public async Task<UserReadDto> CreateUserAsync(UserCreateDto userCreateDto)
        {
            _logger.LogInformation("Creating new user with email: {Email}", userCreateDto.Email);
            var user = new User
            {
                Email = userCreateDto.Email,
                Name = userCreateDto.Name,
                UserName = userCreateDto.Email,
                NormalizedUserName = userCreateDto.Email.ToUpper(),
                NormalizedEmail = userCreateDto.Email.ToUpper(),
                SecurityStamp = Guid.NewGuid().ToString("D")
            };
 
            _mapper.Map(userCreateDto, user);

            var password = $"{user.Name}123";
            user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, password);

            try
            {
                await _userRepository.AddUserAsync(user);
                await _userRepository.AddUserToClientRole(user);
                _logger.LogInformation("User created with ID: {UserId}", user.Id);
                await _emailSender.SendWelcomeEmailAsync(user,password);

            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Failed to create user due to a database update error.");
                if (ex.InnerException is SqlException sqlEx && sqlEx.Number == 2601)
                {
                    throw new DuplicateEmailException("A user with this email already exists.");
                }
                throw;
            }

            var returnUser = _mapper.Map<UserReadDto>(user);
            returnUser.Roles.Clear();
            returnUser.Roles.Add("Client");

            return returnUser;
        }

        public async Task<UserReadDto> UpdateUserAsync(int id, UserUpdateDto userUpdateDto)
        {
            _logger.LogInformation("Updating user with ID: {UserId}", id);
            var user = await _userRepository.GetUserByIdAsync(id) ?? throw new NotFoundException("User not found");
            _mapper.Map(userUpdateDto, user);

            await _userRepository.UpdateUserAsync(user);
            _logger.LogInformation("User updated with ID: {UserId}", user.Id);
            return _mapper.Map<UserReadDto>(user);
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            _logger.LogInformation("Deleting user with ID: {UserId}", id);
            var user = await _userRepository.GetUserByIdAsync(id) ?? throw new NotFoundException("User not found");
            await _userRepository.DeleteUserAsync(user);
            _logger.LogInformation("User deleted with ID: {UserId}", user.Id);
            return true;
        }
    }
}
