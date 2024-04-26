using MassTransit;
using Warehouse.Contracts;

namespace Warehouse.Reporting.Api.Users;

public sealed class UserLoggedConsumer(ILogger<UserLoggedConsumer> logger) : IConsumer<UserLoggedEvent>
{
    public Task Consume(ConsumeContext<UserLoggedEvent> context)
    {
        var userLoggedEvent = context.Message;
        logger.LogInformation("{Consumer} {User} {Date}", nameof(UserLoggedConsumer), userLoggedEvent.UserEmail, userLoggedEvent.LoggedOnUtc);

        return Task.CompletedTask;
    }
}
