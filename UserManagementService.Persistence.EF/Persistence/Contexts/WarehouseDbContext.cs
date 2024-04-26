using Microsoft.EntityFrameworkCore;
using UserManagementService.Domain.Entities.Users;

namespace UserManagementService.Persistence.EF.Persistence.Contexts;

public class WarehouseDbContext : DbContext
{
    public WarehouseDbContext(DbContextOptions options) : base(options)
    {
        Database.Migrate();
    }

    public DbSet<User> Users { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }
    public DbSet<Role> Roles { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(typeof(WarehouseDbContext).Assembly);
        base.OnModelCreating(builder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }
}