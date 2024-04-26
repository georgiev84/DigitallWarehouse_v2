using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserManagementService.Domain.Entities.Users;

namespace Warehouse.Persistence.EF.Persistence.Seeds;

public class SeedDataConfiguration :

    IEntityTypeConfiguration<User>,
    IEntityTypeConfiguration<Role>,
    IEntityTypeConfiguration<UserRole>,

{

    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasData(
            new User
            {
                Id = Guid.Parse("11111111-2222-2321-2321-111111111456"),
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com",
                Password = "password123",
                Phone = "123-456-7890",
                Address = "123 Main Street, City, Country",
                Role = "admin"
            }
        );
    }

    public void Configure(EntityTypeBuilder<Role> builder)
    {
        var roles = new[]
        {
            new Role { Id = Guid.Parse("11111111-2222-2321-3429-111111111456"), Name = "admin" },
            new Role { Id = Guid.Parse("11111111-2222-2321-3529-111111111456"), Name = "customer" }
        };

        builder.HasData(roles);
    }

    public void Configure(EntityTypeBuilder<UserRole> builder)
    {
        var userRole = new UserRole
        {
            UserId = Guid.Parse("11111111-2222-2321-2321-111111111456"),
            RoleId = Guid.Parse("11111111-2222-2321-3429-111111111456")
        };

        builder.HasData(userRole);
    }

}