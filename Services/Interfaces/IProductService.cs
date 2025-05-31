using LemonHiveEcommerce.DTOs;

namespace LemonHiveEcommerce.Services.Interfaces
{
    public interface IProductService : IBaseService<ProductDto>
    {
        Task<PagedResultDto<ProductDto>> GetPagedProductsAsync(string search, int pageIndex, int pageSize);

    }
}
