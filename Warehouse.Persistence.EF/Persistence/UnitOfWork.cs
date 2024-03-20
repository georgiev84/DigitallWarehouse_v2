using Warehouse.Application.Common.Interfaces.Persistence;
using Warehouse.Persistence.EF.Persistence.Contexts;

namespace Warehouse.Persistence.EF.Persistence;

public class UnitOfWork : IUnitOfWork
{
    private readonly WarehouseDbContext _dbContext;
    public IProductRepository Products { get; }
    public ISizeRepository Sizes { get; }
    public IOrderRepository Orders { get; }
    public IBasketRepository Baskets { get; }
    public IBasketLineRepository BasketLines { get; }
    public IProductSizeRepository ProductSizes { get; }
    public IGroupRepository Groups { get; set; }
    public IBrandRepository Brands { get; set; }
    public  IUserRepository Users { get; set; }

    public UnitOfWork(
        WarehouseDbContext dbContext,
        IProductRepository productRepository,
        ISizeRepository sizes,
        IOrderRepository orders,
        IBasketRepository baskets,
        IBasketLineRepository basketLines,
        IProductSizeRepository productSizes,
        IGroupRepository groups,
        IBrandRepository brands,
        IUserRepository users)
    {
        _dbContext = dbContext;
        Products = productRepository;
        Sizes = sizes;
        Orders = orders;
        Baskets = baskets;
        BasketLines = basketLines;
        ProductSizes = productSizes;
        Groups = groups;
        Brands = brands;
        Users = users;
    }

    public async Task<int> SaveAsync()
    {
        return await _dbContext.SaveChangesAsync();
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            _dbContext.Dispose();
        }
    }
}