﻿using Warehouse.Domain.Entities.Users;

namespace Warehouse.Domain.Entities.Orders;

public class Order
{
    public Guid Id { get; set; }
    public Guid StatusId { get; set; }
    public Guid? PaymentId { get; set; }
    public DateTime OrderDate { get; set; }
    public Guid? UserId { get; set; }
    public decimal TotalAmount { get; set; }
    public bool IsDeleted { get; set; }
    public User User { get; set; }
    public ICollection<OrderLine> OrderLines { get; set; }
    public Payment Payment { get; set; }
    public OrderStatus Status { get; set; }
}