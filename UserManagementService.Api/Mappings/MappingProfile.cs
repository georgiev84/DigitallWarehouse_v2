using AutoMapper;
using Microsoft.AspNetCore.Identity.Data;
using UserManagementService.Api.Models.Requests.LoginRequests;
using UserManagementService.Api.Models.Responses.LoginResponses;
using UserManagementService.Application.Features.Queries.Login;
using UserManagementService.Application.Features.Queries.LoginGoogle;
using UserManagementService.Application.Features.Queries.Logout;
using UserManagementService.Application.Features.Queries.Refresh;
using UserManagementService.Application.Models.Dto.Login;

namespace UserManagementService.Api.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        MapFromRequestToQueries();
        MapFromModelsToResponse();
    }

    private void MapFromRequestToQueries()
    {
        CreateMap<LoginRequest, LoginQuery>();
        CreateMap<RefreshTokenRequest, RefreshTokenQuery>();
        CreateMap<LogoutRequest, LogoutQuery>();
        CreateMap<GoogleRequest, LoginGoogleQuery>();
    }

    private void MapFromModelsToResponse()
    {
        CreateMap<LoginModel, LoginResponse>();
        CreateMap<RefreshModel, RefreshTokenResponse>();
        CreateMap<LogoutModel, LogoutResponse>();
        CreateMap<LoginGoogleModel, LoginGoogleResponse>();
    }
}