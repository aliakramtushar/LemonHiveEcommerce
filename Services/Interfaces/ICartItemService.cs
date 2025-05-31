using LemonHiveEcommerce.DTOs;
using LemonHiveEcommerce.Models;

namespace LemonHiveEcommerce.Services.Interfaces
{
    public interface ICartItemService : IBaseService<CartItemDto>
    {
        Task<int> GetCartItemCountAsync();
        Task<IEnumerable<CartItem>> GetAllAsync();
        Task<bool> AddToCartAsync(Guid productId, int qty);
        Task<IEnumerable<CartItemDto>> GetAllWithProductAsync();
    }
}
