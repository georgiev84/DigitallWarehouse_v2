namespace Warehouse.Security.Interfaces;

public interface IDateTimeProvider
{
    DateTime UtcNow { get; }
}