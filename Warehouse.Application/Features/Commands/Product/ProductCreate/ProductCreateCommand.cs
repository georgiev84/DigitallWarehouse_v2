﻿using MediatR;
using Warehouse.Application.Models.Dto;
using Warehouse.Application.Models.Dto.OrderDtos;
using Warehouse.Application.Models.Dto.ProductDtos;

namespace Warehouse.Application.Features.Commands.Product.ProductCreate;
public record ProductCreateCommand(
    Guid BrandId,
    string Title,
    string Description,
    decimal Price,
    List<Guid> GroupIds,
    List<SizeInformationDto> SizeInformation) : IRequest<ProductUpdateDetailsDto>;

