using MediatR;
using Warehouse.Application.Models.Login;

namespace Warehouse.Application.Features.Queries.Refresh;
public record RefreshTokenQuery(string Token, string RefreshToken) : IRequest<RefreshModel>;