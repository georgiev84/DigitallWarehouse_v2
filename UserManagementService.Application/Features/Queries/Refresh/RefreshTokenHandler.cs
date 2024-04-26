using MediatR;
using UserManagementService.Application.Common.Interfaces.Persistence;
using UserManagementService.Application.Common.Interfaces.Security;
using UserManagementService.Application.Extensions;
using UserManagementService.Application.Models.Dto.Login;
using UserManagementService.Domain.Entities.Users;

namespace UserManagementService.Application.Features.Queries.Refresh;

public class RefreshTokenHandler : IRequestHandler<RefreshTokenQuery, RefreshModel>
{
    private readonly IJwtTokenGenerator<User> _jwtTokenGenerator;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDateTimeProvider _dateTimeProvider;

    public RefreshTokenHandler(IJwtTokenGenerator<User> jwtTokenGenerator, IUnitOfWork unitOfWork, IDateTimeProvider dateTimeProvider)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _unitOfWork = unitOfWork;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<RefreshModel> Handle(RefreshTokenQuery request, CancellationToken cancellationToken)
    {
        var tokenId = JwtTokenParser.ParseJwtToken(request.Token);

        var user = await _unitOfWork.Users.GetById(tokenId);

        if (user is null || user.RefreshToken != request.RefreshToken || user.RefreshTokenExpiry < _dateTimeProvider.UtcNow)
        {
            throw new UnauthorizedAccessException();
        }

        var token = _jwtTokenGenerator.GenerateToken(user);

        var refreshModel = new RefreshModel
        {
            Token = token,
            RefreshToken = request.RefreshToken
        };
        return refreshModel;
    }
}