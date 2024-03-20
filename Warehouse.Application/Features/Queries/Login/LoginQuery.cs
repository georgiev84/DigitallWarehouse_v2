using MediatR;
using Warehouse.Application.Models.Login;

namespace Warehouse.Application.Features.Queries.Login;
public record LoginQuery(string Email, string Password) : IRequest<LoginModel>;
