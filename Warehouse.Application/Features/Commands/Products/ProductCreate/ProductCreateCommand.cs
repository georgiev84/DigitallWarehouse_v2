using MediatR;
using Warehouse.Application.Models.Dto;
using Warehouse.Application.Models.Dto.ProductDtos;

namespace Warehouse.Application.Features.Commands.Products.ProductCreate;
public record ProductCreateCommand(
    Guid BrandId,
    string Title,
    string Description,
    decimal Price,
    List<Guid> GroupIds,
    List<SizeInformationDto> Sizes) : IRequest<ProductCreateDetailsDto>;