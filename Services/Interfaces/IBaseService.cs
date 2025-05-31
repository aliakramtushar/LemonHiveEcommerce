using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LemonHiveEcommerce.Services.Interfaces
{
    public interface IBaseService<TDto>
    {
        Task<IEnumerable<TDto>> GetAllAsync();
        Task<TDto?> GetByIdAsync(Guid id);
        Task<bool> AddAsync(TDto dto);
        Task<bool> UpdateAsync(TDto dto);
        Task<bool> DeleteAsync(Guid id);
    }
}
