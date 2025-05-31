using LemonHiveEcommerce.Data;
using LemonHiveEcommerce.DTOs;
using LemonHiveEcommerce.Models;
using LemonHiveEcommerce.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

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
