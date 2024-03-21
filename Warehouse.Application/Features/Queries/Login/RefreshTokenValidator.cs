using FluentValidation;

namespace Warehouse.Application.Features.Queries.Login;
public class RefreshTokenValidator : AbstractValidator<RefreshTokenQuery>
{
    public RefreshTokenValidator()
    {
        RuleFor(x => x.RefreshToken).NotEmpty();
        RuleFor(x => x.Token).NotEmpty();
    }
}