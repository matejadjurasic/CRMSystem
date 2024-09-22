using CRMSystem.Application.Contracts.Persistence;
using CRMSystem.Application.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMSystem.Persistence.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly CRMSystemDbContext _appDbContext;

        public GenericRepository(CRMSystemDbContext appDbContext)
        {
            _appDbContext = appDbContext;  
        }

        public async Task<T> AddAsync(T entity)
        {
            await _appDbContext.AddAsync(entity);
            return entity;
        }

        public Task DeleteAsync(T entity)
        {
            _appDbContext.Set<T>().Remove(entity);
            return Task.CompletedTask;
        }

        public async Task<bool> Exists(int id)
        {
            var entity = await GetAsync(id);
            return entity != null;
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await _appDbContext.Set<T>().ToListAsync();
        }

        public async Task<T> GetAsync(int id)
        {
            var entity = await _appDbContext.Set<T>().FindAsync(id) ?? throw new NotFoundException(typeof(T).Name, id);
            return entity;
        }

        public Task UpdateAsync(T entity)
        {
            _appDbContext.Entry(entity).State = EntityState.Modified;
            return Task.CompletedTask;
        }
    }
}
