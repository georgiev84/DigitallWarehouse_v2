﻿using UserManagementService.Application.Common.Interfaces.Persistence;
using UserManagementService.Persistence.EF.Persistence.Contexts;

namespace UserManagementService.Persistence.EF.Persistence;

public class UnitOfWork : IUnitOfWork
{
    private readonly UsersDbContext _dbContext;
    public IUserRepository Users { get; set; }

    public UnitOfWork(
        UsersDbContext dbContext,
        IUserRepository users)
    {
        Users = users;
        _dbContext = dbContext;
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