using LemonHiveEcommerce.DTOs;
using LemonHiveEcommerce.Models;
using LemonHiveEcommerce.Repositories.Interfaces;

namespace LemonHiveEcommerce.Repositories.Interfaces
{
    public interface ICartItemRepository : IGenericRepository<CartItem>
    {
        //Task<IEnumerable<CartItem>> GetLowStockProductsAsync(int threshold);
    }
}