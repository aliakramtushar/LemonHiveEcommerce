using LemonHiveEcommerce.Data;
using LemonHiveEcommerce.Models;
using LemonHiveEcommerce.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace LemonHiveEcommerce.Repositories.Implementation
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly LemonHiveDbContext _context;
        protected readonly DbSet<T> _dbSet;

        public Repository(LemonHiveDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync() => await _dbSet.ToListAsync();

        public async Task<T?> GetByIdAsync(Guid id) => await _dbSet.FindAsync(id);

        public async Task AddAsync(T entity) => await _dbSet.AddAsync(entity);

        public async Task UpdateAsync(T entity) => _dbSet.Update(entity);

        public async Task DeleteAsync(Guid id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
                _dbSet.Remove(entity);
        }

        //public async Task<IEnumerable<Product>> GetWithRawSql()
        //{
        //    return await _context.Products.FromSqlRaw("SELECT * FROM Products").ToListAsync();
        //}

    }

}
