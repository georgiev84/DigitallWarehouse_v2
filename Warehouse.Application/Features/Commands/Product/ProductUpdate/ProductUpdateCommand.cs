﻿using MediatR;
using Warehouse.Application.Models.Dto;

namespace Warehouse.Application.Features.Commands.Product.Update;
public record ProductUpdateCommand(
    Guid Id,
    Guid BrandId,
    string Title,
    string Description,
    decimal Price,
    List<Guid> GroupIds,
    List<SizeInformationDto> SizeInformation) : IRequest<UpdateProductDetailsDto>;
