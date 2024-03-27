using AutoMapper;
using MediatR;
using Warehouse.Application.Common.Interfaces.Persistence;
using Warehouse.Application.Models.Login;
using Warehouse.Domain.Entities.Users;
using Warehouse.Security.Extensions;
using Warehouse.Security.Interfaces;
using Warehouse.Security.Models;

namespace Warehouse.Application.Features.Queries.Login;
public class LoginGoogleQueryHandler : IRequestHandler<LoginGoogleQuery, LoginGoogleModel>
{
    private readonly IJwtTokenGenerator<User> _jwtTokenGenerator;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IMapper _mapper;

    public LoginGoogleQueryHandler(IJwtTokenGenerator<User> jwtTokenGenerator, IUnitOfWork unitOfWork, IDateTimeProvider dateTimeProvider, IMapper mapper)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _unitOfWork = unitOfWork;
        _dateTimeProvider = dateTimeProvider;
        _mapper = mapper;
    }

    public async Task<LoginGoogleModel> Handle(LoginGoogleQuery query, CancellationToken cancellationToken)
    {
        var email = await AccessTokenDecoder.DecodeAccessTokenAsync(query.Token);

        if(email != query.Email)
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
