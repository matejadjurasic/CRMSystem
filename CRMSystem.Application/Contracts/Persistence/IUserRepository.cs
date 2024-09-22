using CRMSystem.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMSystem.Application.Contracts.Persistence
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User> GetUserByIdAsync(int id);
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task AddUserToClientRole(User user);
    }
}
