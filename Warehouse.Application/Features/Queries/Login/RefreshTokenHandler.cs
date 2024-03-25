﻿using MediatR;
using System.IdentityModel.Tokens.Jwt;
using Warehouse.Application.Common.Interfaces.Persistence;
using Warehouse.Application.Models.Login;
using Warehouse.Domain.Entities.Users;
using Warehouse.Security.Interfaces;

namespace Warehouse.Application.Features.Queries.Login;

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
        JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

        JwtSecurityToken jwtToken = tokenHandler.ReadJwtToken(request.Token);

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