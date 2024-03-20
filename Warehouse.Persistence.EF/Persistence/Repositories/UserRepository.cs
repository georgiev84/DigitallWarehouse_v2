using Microsoft.EntityFrameworkCore;
using Warehouse.Application.Common.Interfaces.Persistence;
using Warehouse.Domain.Entities.Products;
using Warehouse.Domain.Entities.Users;
using Warehouse.Persistence.Abstractions;
using Warehouse.Persistence.EF.Persistence.Contexts;

namespace Warehouse.Persistence.EF.Persistence.Repositories;
public class UserRepository : GenericRepository<User>, IUserRepository
{
    public UserRepository(WarehouseDbContext dbContext) : base(dbContext)
    {
    }
    public async Task<User> GetUserByEmail(string email)
    {
        var user = await _dbContext.Set<User>()
            .Where(u => u.Email == email)
            .FirstOrDefaultAsync();

        if (user == null)
        {
            throw new InvalidOperationException("User with the provided email does not exist.");
        }

        return user;
    }
}
