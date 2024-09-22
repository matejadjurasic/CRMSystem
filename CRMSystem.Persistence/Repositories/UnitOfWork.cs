using CRMSystem.Application.Contracts.Persistence;
using CRMSystem.Domain.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMSystem.Persistence.Repositories
{
    internal class UnitOfWork : IUnitOfWork
    {
        private readonly CRMSystemDbContext _appDbContext;

        private IUserRepository _userRepository;
        private IProjectRepository _projectRepository;
        private UserManager<User> _userManager; 

        public UnitOfWork(CRMSystemDbContext appDbContext, UserManager<User> userManager)
        {
            _appDbContext = appDbContext;
            _userManager = userManager;
        }

        public IProjectRepository ProjectRepository => 
            _projectRepository ??= new ProjectRepository(_appDbContext);

        public IUserRepository UserRepository =>
            _userRepository ??= new UserRepository(_appDbContext,_userManager);

        public void Dispose()
        {
            _appDbContext.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task Save()
        {
            await _appDbContext.SaveChangesAsync();
        }
    }
}
