using Warehouse.Domain.Entities.Products;
using Warehouse.Persistence.Abstractions.Interfaces;

namespace Warehouse.Application.Common.Interfaces.Persistence;

public interface IBrandRepository : IGenericRepository<Brand>
{
}