namespace Warehouse.Domain.Entities.Cache;
public class TokenBlacklistEntry
{
    public string TokenId { get; set; }
    public DateTimeOffset BlacklistedAt { get; set; }
}
