using Warehouse.Application.Models.Dto;
using Warehouse.Application.Models.Dto.ProductDtos;

namespace Warehouse.Api.Models.Responses.ProductResponses;

public class ProductUpdateResponse
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public string Brand { get; set; }
    public Guid BrandId { get; set; }
    public List<GroupDto>? Groups { get; set; }
    public List<SizeDto>? Sizes { get; set; }

    //public Guid Id { get; set; }
    //public string? Title { get; set; }
    //public string? Description { get; set; }
    //public decimal Price { get; set; }
    //public string? Brand { get; set; }
    //public List<string>? Groups { get; set; }
    //public List<SizeResponse>? Sizes { get; set; }
}