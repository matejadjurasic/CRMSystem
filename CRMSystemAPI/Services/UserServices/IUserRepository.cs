using CRMSystemAPI.Models.DatabaseModels;

namespace CRMSystemAPI.Services.UserServices
{
    public interface IUserRepository
    {
        Task<User?> GetUserByIdAsync(int id);
        Task<User?> GetUserByEmailAsync(string email);
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task AddUserAsync(User user);
        Task UpdateUserAsync(User user);
        Task DeleteUserAsync(User user);
        Task AddUserToClientRole(User user);
    }
}
