using Warehouse.Security.Interfaces;

namespace Warehouse.Security.Services;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}