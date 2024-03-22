using MediatR;
using System.IdentityModel.Tokens.Jwt;
using Warehouse.Application.Common.Interfaces.Authentication;
using Warehouse.Application.Common.Interfaces.Persistence;
using Warehouse.Application.Models.Login;
using Warehouse.Application.Services;

namespace Warehouse.Application.Features.Queries.Login;
public class RefreshTokenHandler : IRequestHandler<RefreshTokenQuery, RefreshModel>
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDateTimeProvider _dateTimeProvider;

    public RefreshTokenHandler(IJwtTokenGenerator jwtTokenGenerator, IUnitOfWork unitOfWork, IDateTimeProvider dateTimeProvider)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _unitOfWork = unitOfWork;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<RefreshModel> Handle(RefreshTokenQuery request, CancellationToken cancellationToken)
    {
        var principal = _jwtTokenGenerator.GetPrincipalFromExpiredToken(request.Token);

        JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

        // Read the JWT token
        JwtSecurityToken jwtToken = tokenHandler.ReadJwtToken(request.Token);

        // Get the value of the "sub" claim
        string subClaim = jwtToken.Subject;

        Guid subGuid = Guid.Parse(subClaim);

        var user = await _unitOfWork.Users.GetById(subGuid);

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
