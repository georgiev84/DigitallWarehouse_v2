using MassTransit;
using MediatR;
using Warehouse.Application.Common.Interfaces.Persistence;
using Warehouse.Application.Common.Interfaces.Security;
using Warehouse.Application.Models.Login;
using Warehouse.Contracts;
using Warehouse.Domain.Entities.Users;

namespace Warehouse.Application.Features.Queries.Login;

public class LoginCommandHandler : IRequestHandler<LoginQuery, LoginModel>
{
    private readonly IJwtTokenGenerator<User> _jwtTokenGenerator;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IPublishEndpoint _publishEndpoint;

    public LoginCommandHandler(IJwtTokenGenerator<User> jwtTokenGenerator, IUnitOfWork unitOfWork, IDateTimeProvider dateTimeProvider, IPublishEndpoint publishEndpoint)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _unitOfWork = unitOfWork;
        _dateTimeProvider = dateTimeProvider;
        _publishEndpoint = publishEndpoint;
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

        try
        {
            await _publishEndpoint.Publish(new UserLoggedEvent
            {
                UserId = user.Id,
                UserEmail = user.Email,
                LoggedOnUtc = DateTime.UtcNow
            }, cancellationToken);
        } catch (Exception ex)
        {
            var a = 1;
        }


        var loginModel = new LoginModel
        {
            Email = user.Email,
            Token = token,
            RefreshToken = refreshToken
        };

        return loginModel;
    }
}