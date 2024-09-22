using CRMSystem.Application.Contracts.Persistence;
using CRMSystem.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMSystem.Persistence.Repositories
{
    public class ProjectRepository : GenericRepository<Project>, IProjectRepository
    {
        private readonly CRMSystemDbContext _appDbContext;

        public ProjectRepository(CRMSystemDbContext appDbContext) : base(appDbContext) 
        {
            _appDbContext = appDbContext;
        }

        public async Task<List<Project>> GetProjectsByUserIdAsync(int userId)
        {
            return await _appDbContext.Projects
                .Where(p => p.UserId == userId)
                .ToListAsync();
        }
    }
}
