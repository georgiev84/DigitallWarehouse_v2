﻿using Microsoft.EntityFrameworkCore;
using Warehouse.Application.Common.Interfaces.Persistence;
using Warehouse.Domain.Entities;
using Warehouse.Infrastructure.Persistence.Contexts;

namespace Warehouse.Infrastructure.Persistence.Repositories;
public class BasketLineRepository : GenericRepository<BasketLine>, IBasketLineRepository
{

    protected readonly WarehouseDbContext _dbContext;
    public BasketLineRepository(WarehouseDbContext context, WarehouseDbContext dbContext) : base(context)
    {
        _dbContext = dbContext;
    }

    public async Task BulkDelete(Guid basketId)
    {
        var basketLinesToRemove = await _dbContext.BasketLines
            .Where(bl => bl.BasketId == basketId)
            .ToListAsync();

        _dbContext.BasketLines.RemoveRange(basketLinesToRemove);
    }
}