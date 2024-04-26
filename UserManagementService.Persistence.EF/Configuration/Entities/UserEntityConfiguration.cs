using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserManagementService.Domain.Entities.Users;
using UserManagementService.Persistence.EF.Configuration.Contstants;

namespace UserManagementService.Persistence.EF.Configuration.Entities;

public class UserEntityConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(u => u.Email).HasMaxLength(ColumnTypeConstants.LoginMaxLength).IsRequired();
        builder.Property(u => u.Password).HasMaxLength(ColumnTypeConstants.LoginMaxLength).IsRequired();
        builder.Property(u => u.Address).HasMaxLength(ColumnTypeConstants.AddressMaxLength);
        builder.Property(u => u.Phone).HasMaxLength(ColumnTypeConstants.LoginMaxLength);
        builder.Property(u => u.Role).HasMaxLength(ColumnTypeConstants.RoleMaxLength);
        builder.Property(u => u.RefreshToken).HasMaxLength(ColumnTypeConstants.RefreshTokenMaxLength);
    }
}