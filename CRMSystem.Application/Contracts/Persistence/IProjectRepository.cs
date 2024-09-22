using CRMSystem.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMSystem.Application.Contracts.Persistence
{
    public interface IProjectRepository : IGenericRepository<Project>
    {
        Task<List<Project>> GetProjectsByUserIdAsync(int userId);
    }
}
