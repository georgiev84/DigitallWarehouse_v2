using MediatR;
using Warehouse.Application.Models.Login;

namespace Warehouse.Application.Features.Queries.Logout;
public record LogoutQuery(string Token, string RefreshToken) : IRequest<LogoutModel>;