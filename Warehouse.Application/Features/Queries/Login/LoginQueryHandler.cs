using MediatR;
using Warehouse.Application.Common.Interfaces.Authentication;
using Warehouse.Application.Common.Interfaces.Persistence;
using Warehouse.Application.Models.Login;

namespace Warehouse.Application.Features.Queries.Login;
public class LoginCommandHandler : IRequestHandler<LoginQuery, LoginModel>
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUnitOfWork _unitOfWork;

    public LoginCommandHandler(IJwtTokenGenerator jwtTokenGenerator, IUnitOfWork unitOfWork)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _unitOfWork = unitOfWork;
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

        var loginModel = new LoginModel
        {
            Email = user.Email,
            Token = token
        };

        return loginModel;
    }
}