using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Warehouse.Domain.Entities.Baskets;
using Warehouse.Domain.Entities.Users;
using Warehouse.Persistence.EF.Configuration.Contstants;

namespace Warehouse.Persistence.EF.Configuration.Entities;

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
        builder.HasMany(u => u.Orders)
            .WithOne(u => u.User)
            .HasForeignKey(o => o.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        builder.HasOne(u => u.Basket)
            .WithOne(b => b.User)
            .HasForeignKey<Basket>(b => b.UserId);
    }
}