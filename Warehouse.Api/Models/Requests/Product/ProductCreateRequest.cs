namespace Warehouse.Api.Models.Requests.Product;

public class ProductCreateRequest
{
    public Guid BrandId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public List<Guid> GroupIds { get; set; }
    public List<SizeInformationRequest> Sizes { get; set; }
}