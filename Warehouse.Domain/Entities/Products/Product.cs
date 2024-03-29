﻿using Warehouse.Domain.Entities.Baskets;
using Warehouse.Domain.Entities.Orders;

namespace Warehouse.Domain.Entities.Products;

public class Product
{
    public Guid Id { get; set; }
    public Guid BrandId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public bool IsDeleted { get; set; }
    public ICollection<ProductGroup> ProductGroups { get; set; }
    public ICollection<OrderLine> OrderLines { get; set; }
    public ICollection<ProductSize> ProductSizes { get; set; }
    public ICollection<BasketLine> BasketLines { get; set; }
    public Brand Brand { get; set; }
}