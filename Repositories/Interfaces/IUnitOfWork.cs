using LemonHiveEcommerce.Models;

namespace LemonHiveEcommerce.Repositories.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IProductRepository Products { get; }
        ICartItemRepository CartItems { get; }

        Task<int> SaveAsync();
    }

}
