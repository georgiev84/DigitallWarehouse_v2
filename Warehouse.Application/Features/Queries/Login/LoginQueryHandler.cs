using MediatR;
using Warehouse.Application.Common.Interfaces.Persistence;
using Warehouse.Application.Models.Login;
using Warehouse.Domain.Entities.Users;
using Warehouse.Security.Interfaces;

namespace Warehouse.Application.Features.Queries.Login;

public class LoginCommandHandler : IRequestHandler<LoginQuery, LoginModel>
{
    private readonly IJwtTokenGenerator<User> _jwtTokenGenerator;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDateTimeProvider _dateTimeProvider;

    public LoginCommandHandler(IJwtTokenGenerator<User> jwtTokenGenerator, IUnitOfWork unitOfWork, IDateTimeProvider dateTimeProvider)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _unitOfWork = unitOfWork;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<LoginModel> Handle(LoginQuery query, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.Users.GetUserByEmail(query.Email);

        if (query.Password != user.Password)
        {
            throw new Exception("Invalid password.");
        }

        var token = _jwtTokenGenerator.GenerateToken(user);
        var refreshToken = _jwtTokenGenerator.GenerateRefreshToken();

        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiry = _dateTimeProvider.UtcNow.AddDays(1);

        _unitOfWork.Users.Update(user);
        await _unitOfWork.SaveAsync();

        var loginModel = new LoginModel
        {
            Email = user.Email,
            Token = token,
            RefreshToken = refreshToken
        };

        return loginModel;
    }
}