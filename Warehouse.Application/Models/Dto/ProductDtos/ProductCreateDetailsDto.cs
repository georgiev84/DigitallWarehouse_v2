namespace Warehouse.Application.Models.Dto.ProductDtos;

public class ProductCreateDetailsDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public string Brand { get; set; }
    public Guid BrandId { get; set; }
    public List<GroupDto>? Groups { get; set; }
    public List<SizeDto>? Sizes { get; set; }
}