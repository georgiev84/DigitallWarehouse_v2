using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using UserManagementService.Application.Extensions;
using UserManagementService.Application.Models.Dto;

namespace UserManagementService.Application.Behavior;

public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    where TResponse : UserDto
{
    private readonly IValidator<TRequest> _validator;
    private readonly ILogger<ValidationBehavior<TRequest, TResponse>> _logger;

    public ValidationBehavior(IValidator<TRequest> validator, ILogger<ValidationBehavior<TRequest, TResponse>> logger)
    {
        _validator = validator;
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        LoggingExtensions.LogValidatingRequest(_logger);
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            LoggingExtensions.LogRequestValidationFailed(_logger);
            throw new ValidationException(validationResult.Errors);
        }

        return await next();
    }
}