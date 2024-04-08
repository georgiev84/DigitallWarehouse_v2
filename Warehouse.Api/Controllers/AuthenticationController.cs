using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Warehouse.Api.Models.Requests.Login;
using Warehouse.Api.Models.Responses.LoginResponses;
using Warehouse.Application.Features.Queries.Login;
using Warehouse.Application.Features.Queries.LoginGoogle;
using Warehouse.Application.Features.Queries.Logout;
using Warehouse.Application.Features.Queries.Refresh;

namespace Warehouse.Api.Controllers;

public class AuthenticationController : BaseController
{
    private readonly ISender _mediator;

    public AuthenticationController(ISender mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Login(LoginRequest request, [FromServices] IMapper _mapper)
    {
        var query = _mapper.Map<LoginQuery>(request);

        var result = await _mediator.Send(query);

        if (result is null)
        {
            return Problem(statusCode: StatusCodes.Status401Unauthorized, title: "Error");
        }

        var mappedResult = _mapper.Map<LoginResponse>(result);

        return Ok(mappedResult);
    }

    [HttpPost("refresh")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Refresh(RefreshTokenRequest request, [FromServices] IMapper _mapper)
    {
        var query = _mapper.Map<RefreshTokenQuery>(request);

        var result = await _mediator.Send(query);

        var mappedResult = _mapper.Map<RefreshTokenResponse>(result);

        return Ok(mappedResult);
    }

    [HttpPost("logout")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Logout(LogoutRequest request, [FromServices] IMapper _mapper)
    {
        var query = _mapper.Map<LogoutQuery>(request);

        var result = await _mediator.Send(query);

        var mappedResult = _mapper.Map<LogoutResponse>(result);

        return Ok(mappedResult);
    }

    [HttpPost("google-authenticate")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GoogleAuthenticate(GoogleRequest request, [FromServices] IMapper _mapper)
    {
        var query = _mapper.Map<LoginGoogleQuery>(request);
        var result = await _mediator.Send(query);

        var mappedResult = _mapper.Map<LoginGoogleResponse>(result);

        return Ok(mappedResult);
    }
}