using Warehouse.Application.Common.Interfaces.Persistence;
using Warehouse.Domain.Entities.Products;
using Warehouse.Persistence.Abstractions;
using Warehouse.Persistence.EF.Persistence.Contexts;

namespace Warehouse.Persistence.EF.Persistence.Repositories;
public class BrandRepository : GenericRepository<Brand>, IBrandRepository
{
    public BrandRepository(WarehouseDbContext dbContext) : base(dbContext)
    {
    }
}
