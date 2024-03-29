namespace Warehouse.Application.Common.Interfaces.Security;

public interface IDateTimeProvider
{
    DateTime UtcNow { get; }
}