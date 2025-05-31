using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LemonHiveEcommerce.Repositories.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAll();
        Task<TEntity?> GetById(Guid id);
        Task Add(TEntity entity);
        Task Update(TEntity entity);
        Task Delete(Guid id);
    }
}
