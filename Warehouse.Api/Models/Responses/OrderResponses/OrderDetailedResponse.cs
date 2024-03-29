﻿namespace Warehouse.Api.Models.OrderResponses.Orders;

public class OrderDetailedResponse
{
    public Guid Id { get; set; }
    public string? Status { get; set; }
    public DateTime OrderDate { get; set; }
    public string? FullName { get; set; }
    public decimal TotalAmount { get; set; }

    public List<OrderLineResponse>? OrderLines { get; set; }
}