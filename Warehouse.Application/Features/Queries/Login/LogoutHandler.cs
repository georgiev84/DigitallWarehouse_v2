using MediatR;
using System.IdentityModel.Tokens.Jwt;
using Warehouse.Application.Common.Interfaces.Authentication;
using Warehouse.Application.Common.Interfaces.Persistence;
using Warehouse.Application.Models.Login;
using Warehouse.Application.Services;

namespace Warehouse.Application.Features.Queries.Login;
public class LogoutHandler : IRequestHandler<LogoutQuery, LogoutModel>
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDateTimeProvider _dateTimeProvider;

    public LogoutHandler(IJwtTokenGenerator jwtTokenGenerator, IUnitOfWork unitOfWork, IDateTimeProvider dateTimeProvider)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _unitOfWork = unitOfWork;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<LogoutModel> Handle(LogoutQuery request, CancellationToken cancellationToken)
    {
        JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
        JwtSecurityToken jwtToken = tokenHandler.ReadJwtToken(request.Token);
        string subClaim = jwtToken.Subject;

        Guid subGuid = Guid.Parse(subClaim);

        var user = await _unitOfWork.Users.GetById(subGuid);

        if (user is null)
        {
            throw new UnauthorizedAccessException();
        }

        // invalidate token
        var revokedToken = _jwtTokenGenerator.RevokeToken(user);

        // delete refreshToken from database
        user.RefreshToken = null;
        user.RefreshTokenExpiry = _dateTimeProvider.UtcNow.AddMinutes(-10);
        _unitOfWork.Users.Update(user);
        await _unitOfWork.SaveAsync();

        // return expired token and null refresh token
        var logoutModel = new LogoutModel
        {
            Token = revokedToken,
            RefreshToken = null
        };

        return logoutModel;
    }
}
