using MediatR;
using UserManagementService.Application.Models.Dto.Login;

namespace Warehouse.Application.Features.Queries.Logout;
public record LogoutQuery(string Token, string RefreshToken) : IRequest<LogoutModel>;