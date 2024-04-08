using Warehouse.Application.Common.Interfaces.Security;

namespace Warehouse.Security.Services;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}