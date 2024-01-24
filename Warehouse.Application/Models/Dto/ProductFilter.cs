﻿using Warehouse.Domain.Entities;

namespace Warehouse.Application.Models.Dto;

public class ProductFilter
{
    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }
    public IEnumerable<Size>? AllSizes { get; set; }
    public string[]? CommonWords { get; set; }
}
