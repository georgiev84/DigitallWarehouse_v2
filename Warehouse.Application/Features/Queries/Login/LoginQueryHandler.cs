using MediatR;
using Warehouse.Application.Common.Interfaces.Authentication;
using Warehouse.Application.Common.Interfaces.Persistence;
using Warehouse.Application.Models.Login;
using Warehouse.Application.Services;

namespace Warehouse.Application.Features.Queries.Login;
public class LoginCommandHandler : IRequestHandler<LoginQuery, LoginModel>
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDateTimeProvider _dateTimeProvider;
    public LoginCommandHandler(IJwtTokenGenerator jwtTokenGenerator, IUnitOfWork unitOfWork, IDateTimeProvider dateTimeProvider)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _unitOfWork = unitOfWork;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<LoginModel> Handle(LoginQuery query, CancellationToken cancellationToken)
    {
        // user exist
        var user = await _unitOfWork.Users.GetUserByEmail(query.Email);


        // validate password
        if (query.Password  != user.Password)
        {
            throw new Exception("Invalid password.");
        }

        // create token
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