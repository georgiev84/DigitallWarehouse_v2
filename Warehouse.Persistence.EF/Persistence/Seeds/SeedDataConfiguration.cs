using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Warehouse.Domain.Entities.Baskets;
using Warehouse.Domain.Entities.Orders;
using Warehouse.Domain.Entities.Products;
using Warehouse.Domain.Entities.Users;
using Warehouse.Domain.Enums;

namespace Warehouse.Persistence.EF.Persistence.Seeds;

public class SeedDataConfiguration :
    IEntityTypeConfiguration<Brand>,
    IEntityTypeConfiguration<Size>,
    IEntityTypeConfiguration<OrderStatus>,
    IEntityTypeConfiguration<User>,
    IEntityTypeConfiguration<Basket>,
    IEntityTypeConfiguration<Role>,
    IEntityTypeConfiguration<UserRole>,
    IEntityTypeConfiguration<Product>,
    IEntityTypeConfiguration<Group>,
    IEntityTypeConfiguration<ProductGroup>,
    IEntityTypeConfiguration<ProductSize>,
    IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Brand> builder)
    {
        builder.HasData(
            new Brand { Id = Guid.Parse("11111111-1111-1111-1111-111111111111"), Name = "Adidas" },
            new Brand { Id = Guid.Parse("22222222-2222-2222-2222-222222222222"), Name = "Nike" }
        );
    }

    public void Configure(EntityTypeBuilder<Size> builder)
    {
        builder.HasData(
            new Size { Id = Guid.Parse("33333333-3333-3333-3333-333333333333"), Name = "Small" },
            new Size { Id = Guid.Parse("44444444-4444-4444-4444-444444444444"), Name = "Medium" },
            new Size { Id = Guid.Parse("55555555-5555-5555-5555-555555555555"), Name = "Large" }
        );
    }

    public void Configure(EntityTypeBuilder<OrderStatus> builder)
    {
        builder.HasData(
            new OrderStatus { Id = Guid.Parse("11111111-2222-2321-2321-111111111111"), Name = OrderStatusName.Pending },
            new OrderStatus { Id = Guid.Parse("22222222-1111-1234-4321-222222222222"), Name = OrderStatusName.Processing },
            new OrderStatus { Id = Guid.Parse("33333333-3322-1122-4444-333333333333"), Name = OrderStatusName.Shipped },
            new OrderStatus { Id = Guid.Parse("44444444-5555-5555-6666-666666666666"), Name = OrderStatusName.Completed },
            new OrderStatus { Id = Guid.Parse("77777777-7777-7777-8888-888888888888"), Name = OrderStatusName.Canceled }
        );
    }

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
                Role = "admin",
                Orders = new List<Order>()
            }
        );
    }

    public void Configure(EntityTypeBuilder<Basket> builder)
    {
        builder.HasData(
             new Basket
             {
                 Id = Guid.Parse("11111111-2222-2321-2321-111111111429"),
                 UserId = Guid.Parse("11111111-2222-2321-2321-111111111456"),
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

    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasData(
            new Product
            {
                Id = Guid.Parse("88888888-8888-8888-8888-888888888888"),
                Title = "Product 3",
                Description = "Description for Product 3",
                Price = 19.99m,
                BrandId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
            },
            new Product
            {
                Id = Guid.Parse("99999999-9999-9999-9999-999999999999"),
                Title = "Product 4",
                Description = "Description for Product 4",
                Price = 49.99m,
                BrandId = Guid.Parse("22222222-2222-2222-2222-222222222222"),
            },
            new Product
            {
                Id = Guid.Parse("12345678-1234-5678-9012-345678901234"),
                Title = "Product 5",
                Description = "Description for Product 5",
                Price = 59.99m,
                BrandId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
            },
            new Product
            {
                Id = Guid.Parse("23456789-2345-6789-0123-456789012345"),
                Title = "Classic Jeans",
                Description = "Timeless classic jeans in blue for a casual and versatile look.",
                Price = 34.99m,
                BrandId = Guid.Parse("22222222-2222-2222-2222-222222222222"),
            }
        );
    }

    public void Configure(EntityTypeBuilder<Group> builder)
    {
        builder.HasData(
            new Group { Id = Guid.Parse("88888888-8888-8888-8888-888888888888"), Name = "Male" },
            new Group { Id = Guid.Parse("99999999-9999-9999-9999-999999999999"), Name = "Female" }
        );
    }

    public void Configure(EntityTypeBuilder<ProductGroup> builder)
    {
        builder.HasData(
            new ProductGroup { ProductId = Guid.Parse("88888888-8888-8888-8888-888888888888"), GroupId = Guid.Parse("99999999-9999-9999-9999-999999999999") },
            new ProductGroup { ProductId = Guid.Parse("99999999-9999-9999-9999-999999999999"), GroupId = Guid.Parse("88888888-8888-8888-8888-888888888888") },
            new ProductGroup { ProductId = Guid.Parse("12345678-1234-5678-9012-345678901234"), GroupId = Guid.Parse("88888888-8888-8888-8888-888888888888") },
            new ProductGroup { ProductId = Guid.Parse("23456789-2345-6789-0123-456789012345"), GroupId = Guid.Parse("99999999-9999-9999-9999-999999999999") }
        );
    }

    public void Configure(EntityTypeBuilder<ProductSize> builder)
    {
        builder.HasData(
            new ProductSize { ProductId = Guid.Parse("88888888-8888-8888-8888-888888888888"), SizeId = Guid.Parse("55555555-5555-5555-5555-555555555555"), QuantityInStock = 30 },
            new ProductSize { ProductId = Guid.Parse("88888888-8888-8888-8888-888888888888"), SizeId = Guid.Parse("44444444-4444-4444-4444-444444444444"), QuantityInStock = 20 },
            new ProductSize { ProductId = Guid.Parse("99999999-9999-9999-9999-999999999999"), SizeId = Guid.Parse("55555555-5555-5555-5555-555555555555"), QuantityInStock = 30 },
            new ProductSize { ProductId = Guid.Parse("12345678-1234-5678-9012-345678901234"), SizeId = Guid.Parse("55555555-5555-5555-5555-555555555555"), QuantityInStock = 30 },
            new ProductSize { ProductId = Guid.Parse("12345678-1234-5678-9012-345678901234"), SizeId = Guid.Parse("44444444-4444-4444-4444-444444444444"), QuantityInStock = 20 },
            new ProductSize { ProductId = Guid.Parse("23456789-2345-6789-0123-456789012345"), SizeId = Guid.Parse("55555555-5555-5555-5555-555555555555"), QuantityInStock = 30 },
            new ProductSize { ProductId = Guid.Parse("23456789-2345-6789-0123-456789012345"), SizeId = Guid.Parse("44444444-4444-4444-4444-444444444444"), QuantityInStock = 20 }
        );
    }

    public void Configure(EntityTypeBuilder<Payment> builder)
    {
    }
}