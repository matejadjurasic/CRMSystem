using CRMSystemAPI.Models.DataTransferModels.UserTransferModels;

namespace CRMSystemAPI.Services.UserServices
{
    public interface IUserService
    {
        Task<IEnumerable<UserReadDto>> GetUsersAsync();
        Task<UserReadDto> GetUserByIdAsync(int id);
        Task<UserReadDto> CreateUserAsync(UserCreateDto userCreateDto);
        Task<UserReadDto> UpdateUserAsync(int id, UserUpdateDto userUpdateDto);
        Task<bool> DeleteUserAsync(int id);
    }
}
