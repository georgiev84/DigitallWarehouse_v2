using FluentValidation;

namespace Warehouse.Application.Features.Queries.Refresh;

public class RefreshTokenValidator : AbstractValidator<RefreshTokenQuery>
{
    public RefreshTokenValidator()
    {
        RuleFor(x => x.RefreshToken).NotEmpty();
        RuleFor(x => x.Token).NotEmpty();
    }
}