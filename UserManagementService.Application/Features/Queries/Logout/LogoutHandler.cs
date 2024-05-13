using MediatR;
using System.IdentityModel.Tokens.Jwt;
using UserManagementService.Application.Models.Dto.Login;
using UserManagementService.Application.Common.Interfaces.Persistence;
using UserManagementService.Application.Common.Interfaces.Security;
using MassTransit;
using Warehouse.Persistence.Abstractions;
//using UserManagementService.Application.Models.Events;

namespace UserManagementService.Application.Features.Queries.Logout;

public class LogoutHandler : IRequestHandler<LogoutQuery, LogoutModel>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly ITokenBlacklistService _tokenBlackListService;
    private readonly IPublishEndpoint _publishEndpoint;

    public LogoutHandler(IUnitOfWork unitOfWork, IDateTimeProvider dateTimeProvider, ITokenBlacklistService tokenBlackListService, IPublishEndpoint publishEndpoint)
    {
        _unitOfWork = unitOfWork;
        _dateTimeProvider = dateTimeProvider;
        _tokenBlackListService = tokenBlackListService;
        _publishEndpoint = publishEndpoint;
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

        try
        {
            await _publishEndpoint.Publish(new TokenBlacklistEvent
            {
                BlacklistedToken = tokenId
            });

        } catch(Exception ex)
        {
            var a = 1;
        }


        var logoutModel = new LogoutModel
        {
            Token = request.Token,
            RefreshToken = null
        };

        return logoutModel;
    }
}