using FluentValidation;

namespace UserManagementService.Application.Features.Queries.Refresh;

public class RefreshTokenValidator : AbstractValidator<RefreshTokenQuery>
{
    public RefreshTokenValidator()
    {
        RuleFor(x => x.RefreshToken).NotEmpty();
        RuleFor(x => x.Token).NotEmpty();
    }
}