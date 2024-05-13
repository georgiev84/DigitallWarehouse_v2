namespace Warehouse.Persistence.Abstractions;
public record TokenBlacklistEvent
{
    public string BlacklistedToken { get; set; }
}