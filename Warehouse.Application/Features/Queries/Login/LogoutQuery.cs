using MediatR;
using Warehouse.Application.Models.Login;

namespace Warehouse.Application.Features.Queries.Login;
public record LogoutQuery(string Token, string RefreshToken) : IRequest<LogoutModel>;