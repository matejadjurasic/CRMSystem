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

        public UserService(IUserRepository userRepository, IMapper mapper, UserManager<User> userManager, IEmailSender emailSender)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _userManager = userManager;
            _emailSender = emailSender;
        }

        public async Task<IEnumerable<UserReadDto>> GetUsersAsync()
        {
            var users = await _userRepository.GetAllUsersAsync();
            return _mapper.Map<IEnumerable<UserReadDto>>(users);
        }

        public async Task<UserReadDto> GetUserByIdAsync(int id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            if (user == null) throw new NotFoundException("User not found");
            return _mapper.Map<UserReadDto>(user);
        }

        public async Task<UserReadDto> CreateUserAsync(UserCreateDto userCreateDto)
        {
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

                await _emailSender.SendWelcomeEmailAsync(user,password);

            }
            catch (DbUpdateException ex)
            {
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
            var user = await _userRepository.GetUserByIdAsync(id) ?? throw new NotFoundException("User not found");
            _mapper.Map(userUpdateDto, user);

            await _userRepository.UpdateUserAsync(user);
            return _mapper.Map<UserReadDto>(user);
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await _userRepository.GetUserByIdAsync(id) ?? throw new NotFoundException("User not found");
            await _userRepository.DeleteUserAsync(user);
            return true;
        }
    }
}
