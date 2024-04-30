using Microsoft.EntityFrameworkCore;
using UserManagementService.Application.Common.Interfaces.Persistence;
using UserManagementService.Domain.Entities.Users;
using UserManagementService.Persistence.EF.Persistence.Contexts;
using Warehouse.Persistence.Abstractions;

namespace UserManagementService.Persistence.EF.Persistence.Repositories;

public class UserRepository : GenericRepository<User>, IUserRepository
{
    public UserRepository(UsersDbContext dbContext) : base(dbContext)
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