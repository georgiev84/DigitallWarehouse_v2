﻿namespace Warehouse.Api.Models.Requests.Product;

public class ProductFilterRequest
{
    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }
    public string? Highlight { get; set; }
    public string? Size { get; set; }
}