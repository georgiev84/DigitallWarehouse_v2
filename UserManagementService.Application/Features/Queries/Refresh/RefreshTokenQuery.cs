using MediatR;
using UserManagementService.Application.Models.Dto.Login;

namespace UserManagementService.Application.Features.Queries.Refresh;
public record RefreshTokenQuery(string Token, string RefreshToken) : IRequest<RefreshModel>;