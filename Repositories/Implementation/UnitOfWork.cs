using LemonHiveEcommerce.Data;
using LemonHiveEcommerce.Models;
using LemonHiveEcommerce.Repositories.Interfaces;
using System.Threading.Tasks;

namespace LemonHiveEcommerce.Repositories.Implementation
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly LemonHiveDbContext _context;
        private IProductRepository? _products;
        private ICartItemRepository? _cartItems;

        public UnitOfWork(LemonHiveDbContext context)
        {
            _context = context;
        }

        public IProductRepository Products => _products ??= new ProductRepository(_context);
        public ICartItemRepository CartItems => _cartItems ??= new CartItemRepository(_context);

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

    }
}
