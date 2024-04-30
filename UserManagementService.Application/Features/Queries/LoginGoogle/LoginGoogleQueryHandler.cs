﻿using AutoMapper;
using MediatR;
using UserManagementService.Application.Common.Interfaces.Security;
using UserManagementService.Application.Models.Dto;
using UserManagementService.Application.Models.Dto.Login;
using UserManagementService.Domain.Entities.Users;

namespace UserManagementService.Application.Features.Queries.LoginGoogle;

public class LoginGoogleQueryHandler : IRequestHandler<LoginGoogleQuery, LoginGoogleModel>
{
    private readonly IJwtTokenGenerator<User> _jwtTokenGenerator;
    private readonly IMapper _mapper;

    public LoginGoogleQueryHandler(IJwtTokenGenerator<User> jwtTokenGenerator, IMapper mapper)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _mapper = mapper;
    }

    public async Task<LoginGoogleModel> Handle(LoginGoogleQuery query, CancellationToken cancellationToken)
    {
        var email = await AccessTokenDecoder.DecodeAccessTokenAsync(query.Token);

        if (email != query.Email)
        {
            throw new ArgumentException("Provided token is not valid");
        }

        var userInfo = _mapper.Map<GoogleUserInfo>(query);

        var token = _jwtTokenGenerator.GenerateTokenWithGoogle(userInfo);

        var loginGoogleModel = new LoginGoogleModel
        {
            Email = query.Email,
            Token = token,
            Name = query.Name
        };

        return loginGoogleModel;
    }
}