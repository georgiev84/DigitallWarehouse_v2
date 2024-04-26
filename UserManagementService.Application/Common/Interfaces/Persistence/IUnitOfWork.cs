namespace UserManagementService.Application.Common.Interfaces.Persistence;

public interface IUnitOfWork : IDisposable
{
    IUserRepository Users { get; }

    Task<int> SaveAsync();
}