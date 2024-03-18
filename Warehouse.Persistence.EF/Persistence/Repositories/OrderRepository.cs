using Microsoft.EntityFrameworkCore;
using Warehouse.Application.Common.Interfaces.Persistence;
using Warehouse.Domain.Entities.Orders;
using Warehouse.Domain.Exceptions.OrderExceptions;
using Warehouse.Persistence.Abstractions;
using Warehouse.Persistence.EF.Persistence.Contexts;

namespace Warehouse.Persistence.EF.Persistence.Repositories;

public class OrderRepository : GenericRepository<Order>, IOrderRepository
{
    public OrderRepository(WarehouseDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<Order> GetSingleOrderAsync(Guid orderId)
    {
        try
        {
            var result = await _dbContext.Set<Order>()
                .Include(o => o.User)
                .Include(o => o.Status)
                .Include(o => o.OrderLines)
                    .ThenInclude(od => od.Product)
                    .ThenInclude(p => p.ProductSizes)
                    .ThenInclude(ps => ps.Size)
                .Where(p => p.IsDeleted == false)
                .SingleAsync(o => o.Id == orderId);

            return result;
        }
        catch (InvalidOperationException ex)
        {
            throw new OrderNotFoundException($"Order with ID {orderId} not found.");
        }
        catch
        {
            throw;
        }
    }

    public async Task<IEnumerable<Order>> GetOrdersListAsync()
    {
        var result = await _dbContext.Set<Order>()
            .Include(o => o.User)
            .Include(o => o.Status)
            .Where(p => p.IsDeleted == false)
            .ToListAsync();

        return result;
    }
}