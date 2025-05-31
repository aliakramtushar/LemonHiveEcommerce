using AutoMapper;
using LemonHiveEcommerce.DTOs;
using LemonHiveEcommerce.Models;
using LemonHiveEcommerce.Repositories.Interfaces;
using LemonHiveEcommerce.Services.Interfaces;

namespace LemonHiveEcommerce.Services.Implementations
{
    public class ProductService : BaseService<ProductDto, Product>, IProductService
    {
        public ProductService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        protected override IGenericRepository<Product> Repository => _unitOfWork.Products;

        public async Task<PagedResultDto<ProductDto>> GetPagedProductsAsync(string search, int pageIndex, int pageSize)
        {
            var result = await _unitOfWork.Products.GetPagedProductsAsync(search, pageIndex, pageSize);
            var mappedItems = _mapper.Map<IEnumerable<ProductDto>>(result.Items);

            return new PagedResultDto<ProductDto>
            {
                Items = mappedItems,
                TotalCount = result.TotalCount,
                PageIndex = result.PageIndex,
                PageSize = result.PageSize
            };
        }
    }
}
