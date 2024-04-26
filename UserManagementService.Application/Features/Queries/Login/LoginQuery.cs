using MediatR;
using UserManagementService.Application.Models.Dto.Login;

namespace UserManagementService.Application.Features.Queries.Login;
public record LoginQuery(string Email, string Password) : IRequest<LoginModel>;