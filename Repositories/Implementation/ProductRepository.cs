using LemonHiveEcommerce.Data;
using LemonHiveEcommerce.DTOs;
using LemonHiveEcommerce.Models;
using LemonHiveEcommerce.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LemonHiveEcommerce.Repositories.Implementation
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly LemonHiveDbContext _context;

        public ProductRepository(LemonHiveDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<PagedResultDto<Product>> GetPagedProductsAsync(string search, int pageIndex, int pageSize)
        {
            var query = _context.Products.AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(p => p.ProductName.Contains(search));
            }

            var totalCount = await query.CountAsync();

            var items = await query
                .OrderBy(p => p.ProductName)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResultDto<Product>
            {
                Items = items,
                TotalCount = totalCount,
                PageIndex = pageIndex,
                PageSize = pageSize
            };
        }

        public async Task<IEnumerable<Product>> GetByIds(IEnumerable<Guid> ids)
        {
            return await _context.Products
                .Where(p => ids.Contains(p.Id))
                .ToListAsync();
        }
    }
}
