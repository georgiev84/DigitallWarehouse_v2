namespace UserManagementService.Application.Models.Events;
public record TokenBlacklistEvent
{
    public string BlacklistedToken { get; set; }
}
