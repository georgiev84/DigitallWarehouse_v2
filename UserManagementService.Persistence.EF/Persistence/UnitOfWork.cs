using UserManagementService.Application.Common.Interfaces.Persistence;
using UserManagementService.Persistence.EF.Persistence.Contexts;
using Warehouse.Application.Common.Interfaces.Persistence;
using Warehouse.Persistence.EF.Persistence.Contexts;

namespace Warehouse.Persistence.EF.Persistence;

public class UnitOfWork : IUnitOfWork
{
    private readonly WarehouseDbContext _dbContext;
    public IUserRepository Users { get; set; }

    public UnitOfWork(
        WarehouseDbContext dbContext,
        IUserRepository users)
    {
        _dbContext = dbContext;
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