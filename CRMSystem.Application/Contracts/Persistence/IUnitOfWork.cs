using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMSystem.Application.Contracts.Persistence
{
    public interface IUnitOfWork : IDisposable
    {
        IProjectRepository ProjectRepository { get; }
        IUserRepository UserRepository { get; }
        Task Save();
    }
}
