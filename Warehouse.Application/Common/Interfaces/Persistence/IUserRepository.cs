using Warehouse.Domain.Entities.Users;
using Warehouse.Persistence.Abstractions.Interfaces;

namespace Warehouse.Application.Common.Interfaces.Persistence;

public interface IUserRepository : IGenericRepository<User>
{
    Task<User> GetUserByEmail(string email);
}