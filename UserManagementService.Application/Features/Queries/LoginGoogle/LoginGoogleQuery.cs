using MediatR;
using UserManagementService.Application.Models.Dto.Login;

namespace UserManagementService.Application.Features.Queries.LoginGoogle;
public record LoginGoogleQuery(
    string Id,
    string Token,
    string Email,
    string Name,
    string Picture) : IRequest<LoginGoogleModel>;