using Warehouse.Security.Models;

namespace Warehouse.Api.Models.Requests.Login;

public class GoogleRequest
{
    public string Token { get; set; }
    public string Id { get; set; }
    public string Email { get; set; }
    public string Name { get; set; }
    public string Picture { get; set; }
}
