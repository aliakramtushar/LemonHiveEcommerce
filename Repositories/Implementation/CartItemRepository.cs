using LemonHiveEcommerce.Data;
using LemonHiveEcommerce.Models;
using LemonHiveEcommerce.Repositories.Interfaces;

namespace LemonHiveEcommerce.Repositories.Implementation
{
    public class CartItemRepository : GenericRepository<CartItem>, ICartItemRepository
    {
        private readonly LemonHiveDbContext _context;

        public CartItemRepository(LemonHiveDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
