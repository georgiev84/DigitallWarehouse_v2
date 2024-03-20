namespace Warehouse.Application.Common.Interfaces.Persistence;

public interface IUnitOfWork : IDisposable
{
    IProductRepository Products { get; }
    ISizeRepository Sizes { get; }
    IOrderRepository Orders { get; }
    IBasketRepository Baskets { get; }
    IBasketLineRepository BasketLines { get; }
    IProductSizeRepository ProductSizes { get; }
    IGroupRepository Groups { get; }
    IBrandRepository Brands { get; }
    IUserRepository Users { get; }
    Task<int> SaveAsync();
}