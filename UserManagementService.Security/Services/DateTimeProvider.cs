using UserManagementService.Application.Common.Interfaces.Security;

namespace UserManagementService.Security.Services;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}