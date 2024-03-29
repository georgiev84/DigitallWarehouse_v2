﻿namespace Warehouse.Api.Models.Responses.OrderResponses;

public class OrderResponse
{
    public Guid Id { get; set; }
    public Guid PaymentId { get; set; }
    public string? Status { get; set; }
    public DateTime OrderDate { get; set; }
    public string? FullName { get; set; }
    public decimal TotalAmount { get; set; }
}