using Microsoft.Extensions.Logging;

namespace UserManagementService.Application.Extensions;

public static partial class LoggingExtensions
{
    [LoggerMessage(EventId = 1, Level = LogLevel.Information, Message = "Send request {RequestName}, {DateTimeUtc}")]
    public static partial void LogSendRequest(this ILogger logger, string RequestName, DateTime DateTimeUtc);

    [LoggerMessage(EventId = 2, Level = LogLevel.Information, Message = "Complete request {RequestName}, {DateTimeUtc}")]
    public static partial void LogCompleteRequest(this ILogger logger, string RequestName, DateTime DateTimeUtc);

    [LoggerMessage(EventId = 3, Level = LogLevel.Error, Message = "Error occurred while handling {RequestName}, {DateTimeUtc}, with Exception: {ExceptionMessage}")]
    public static partial void LogErrorRequest(this ILogger logger, string RequestName, DateTime DateTimeUtc, string ExceptionMessage);

    [LoggerMessage(EventId = 4, Level = LogLevel.Information, Message = "Getting Products from Database...")]
    public static partial void LogGettingProducts(this ILogger logger);

    [LoggerMessage(EventId = 5, Level = LogLevel.Error, Message = "Failed to retrieve products from the database.")]
    public static partial void LogErrorFetchingProducts(this ILogger logger);

    [LoggerMessage(EventId = 6, Level = LogLevel.Information, Message = "Filtering Products...")]
    public static partial void LogFilteringProducts(this ILogger logger);

    [LoggerMessage(EventId = 7, Level = LogLevel.Error, Message = "An error occurred while getting products: {Message}")]
    public static partial void LogErrorFetchingProducts(this ILogger logger, string Message);

    [LoggerMessage(EventId = 8, Level = LogLevel.Information, Message = "Validating request...")]
    public static partial void LogValidatingRequest(this ILogger logger);

    [LoggerMessage(EventId = 9, Level = LogLevel.Error, Message = "Request Validation Failed.")]
    public static partial void LogRequestValidationFailed(this ILogger logger);

    [LoggerMessage(EventId = 14, Level = LogLevel.Error, Message = "{Message}")]
    public static partial void LogProductCreationFailed(this ILogger logger, string Message, Exception ex);

}