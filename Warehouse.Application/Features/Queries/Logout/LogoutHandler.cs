using MediatR;
using System.IdentityModel.Tokens.Jwt;
using Warehouse.Application.Common.Interfaces.Persistence;
using Warehouse.Application.Common.Interfaces.Security;
using Warehouse.Application.Models.Login;

namespace Warehouse.Application.Features.Queries.Logout;

public class LogoutHandler : IRequestHandler<LogoutQuery, LogoutModel>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly ITokenBlacklistService _tokenBlackListService;

    public LogoutHandler(IUnitOfWork unitOfWork, IDateTimeProvider dateTimeProvider, ITokenBlacklistService tokenBlackListService)
    {
        _unitOfWork = unitOfWork;
        _dateTimeProvider = dateTimeProvider;
        _tokenBlackListService = tokenBlackListService;
    }

    public async Task<LogoutModel> Handle(LogoutQuery request, CancellationToken cancellationToken)
    {
        JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
        JwtSecurityToken jwtToken = tokenHandler.ReadJwtToken(request.Token);
        string subClaim = jwtToken.Subject;
        string tokenId = jwtToken.Id;
        Guid subGuid = Guid.Parse(subClaim);

        var user = await _unitOfWork.Users.GetById(subGuid);

        if (user is null)
        {
            throw new UnauthorizedAccessException();
        }

        await _tokenBlackListService.BlacklistToken(tokenId);

        user.RefreshToken = null;
        user.RefreshTokenExpiry = _dateTimeProvider.UtcNow.AddMinutes(-10);
        _unitOfWork.Users.Update(user);
        await _unitOfWork.SaveAsync();

        var logoutModel = new LogoutModel
        {
            Token = request.Token,
            RefreshToken = null
        };

        return logoutModel;
    }
}