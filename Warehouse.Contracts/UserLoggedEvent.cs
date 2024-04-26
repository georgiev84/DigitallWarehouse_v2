namespace Warehouse.Contracts;

public record UserLoggedEvent
{
    public Guid UserId { get; set; }
    public string UserEmail { get; set; }
    public DateTime LoggedOnUtc { get; set; }
}