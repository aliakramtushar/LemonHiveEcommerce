using LemonHiveEcommerce.Data;
using LemonHiveEcommerce.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LemonHiveEcommerce.Repositories.Implementation
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        protected readonly LemonHiveDbContext _context;
        protected readonly DbSet<TEntity> _dbSet;

        public GenericRepository(LemonHiveDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<TEntity?> GetById(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task Add(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public Task Update(TEntity entity)
        {
            _dbSet.Update(entity);
            return Task.CompletedTask;
        }

        public async Task Delete(Guid id)
        {
            var entity = await GetById(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
            }
        }
    }
}
