using AutoMapper;
using LemonHiveEcommerce.DTOs;
using LemonHiveEcommerce.Models;
using LemonHiveEcommerce.Repositories.Interfaces;
using LemonHiveEcommerce.Services.Interfaces;

namespace LemonHiveEcommerce.Services.Implementations
{
    public class CartItemService : BaseService<CartItemDto, CartItem>, ICartItemService
    {
        public CartItemService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        protected override IGenericRepository<CartItem> Repository => _unitOfWork.CartItems;

        public async Task<int> GetCartItemCountAsync()
        {
            var items = await _unitOfWork.CartItems.GetAll();
            return items.Count();
        }

        public async Task<IEnumerable<CartItem>> GetAllAsync()
        {
            return await _unitOfWork.CartItems.GetAll();
        }

        public async Task<IEnumerable<CartItemDto>> GetAllWithProductAsync()
        {
            var cartItems = await _unitOfWork.CartItems.GetAll();
            var productIds = cartItems.Select(ci => ci.ProductId).Distinct().ToList();
            var products = await _unitOfWork.Products.GetByIds(productIds);

            var result = from cart in cartItems
                         join product in products on cart.ProductId equals product.Id
                         select new CartItemDto
                         {
                             Id = cart.Id,
                             ProductId = cart.ProductId,
                             ProductName = product.ProductName,
                             Qty = cart.Qty,
                             CreatedDate = cart.CreatedDate,
                             TotalPrice = cart.Qty * product.Price,
                         };

            return result;
        }


        public async Task<bool> AddToCartAsync(Guid productId, int qty)
        {
            if (productId == Guid.Empty || qty <= 0)
                throw new ArgumentException("Invalid product ID or quantity.");

            var product = await _unitOfWork.Products.GetById(productId);
            if (product == null)
                throw new InvalidOperationException("Product not found.");

            var cartItem = new CartItem
            {
                Id = Guid.NewGuid(),
                ProductId = productId,
                Qty = qty,
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow
            };

            await _unitOfWork.CartItems.Add(cartItem);
            await _unitOfWork.SaveAsync();
            return true;
        }

    }
}
