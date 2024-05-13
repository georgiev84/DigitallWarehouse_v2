namespace Warehouse.Api.Models.Broker;

public record TokenBlacklistEvent
{
    public string BlacklistedToken { get; set; }
}