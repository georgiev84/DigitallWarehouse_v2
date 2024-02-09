﻿using Warehouse.Application.Models.Dto;

namespace Warehouse.Api.Models.Requests.BasketLine;

public class BasketLineCreateRequest
{
    public Guid UserId { get; set; }
    public BasketLineDto? BasketLine { get; set; }
}