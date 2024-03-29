using MediatR;
using Warehouse.Application.Models.Login;

namespace Warehouse.Application.Features.Queries.LoginGoogle;
public record LoginGoogleQuery(
    string Id,
    string Token,
    string Email,
    string Name,
    string Picture) : IRequest<LoginGoogleModel>;