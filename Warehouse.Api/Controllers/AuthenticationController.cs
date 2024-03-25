using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Warehouse.Api.Models.Requests.Login;
using Warehouse.Api.Models.Responses.LoginResponses;
using Warehouse.Application.Features.Queries.Login;

namespace Warehouse.Api.Controllers;

public class AuthenticationController : BaseController
{
    private readonly ISender _mediator;

    public AuthenticationController(ISender mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request, [FromServices] IMapper _mapper)
    {
        var query = new LoginQuery(request.Email, request.Password);

        var result = await _mediator.Send(query);

        if (result is null)
        {
            return Problem(statusCode: StatusCodes.Status401Unauthorized, title: "Error");
        }

        var mappedResult = _mapper.Map<LoginResponse>(result);

        return Ok(mappedResult);
    }

    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh(RefreshTokenRequest request, [FromServices] IMapper _mapper)
    {
        var query = new RefreshTokenQuery(request.Token, request.RefreshToken);

        var result = await _mediator.Send(query);

        var mappedResult = _mapper.Map<RefreshTokenResponse>(result);

        return Ok(mappedResult);
    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout(LogoutRequest request, [FromServices] IMapper _mapper)
    {
        var query = new LogoutQuery(request.Token, request.RefreshToken);

        var result = await _mediator.Send(query);

        var mappedResult = _mapper.Map<LogoutResponse>(result);

        return Ok(mappedResult);
    }
}