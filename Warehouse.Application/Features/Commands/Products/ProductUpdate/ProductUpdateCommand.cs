using MediatR;
using Warehouse.Application.Models.Dto;
using Warehouse.Application.Models.Dto.ProductDtos;

namespace Warehouse.Application.Features.Commands.Products.Update;
public record ProductUpdateCommand(
    Guid Id,
    Guid BrandId,
    string Title,
    string Description,
    decimal Price,
    List<Guid> GroupIds,
    List<SizeInformationDto> Sizes) : IRequest<ProductUpdateDetailsDto>;