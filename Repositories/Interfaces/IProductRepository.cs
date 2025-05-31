using LemonHiveEcommerce.DTOs;
using LemonHiveEcommerce.Models;

namespace LemonHiveEcommerce.Repositories.Interfaces
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<PagedResultDto<Product>> GetPagedProductsAsync(string search, int pageIndex, int pageSize);
        Task<IEnumerable<Product>> GetByIds(IEnumerable<Guid> ids);
    }
}
