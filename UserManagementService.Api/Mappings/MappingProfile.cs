using AutoMapper;
using Microsoft.AspNetCore.Identity.Data;
using UserManagementService.Application.Features.Queries.Login;

namespace UserManagementService.Api.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        MapFromRequestToCommands();
        MapFromDtoToResponse();
        MapFromRequestToQueries();
        MapFromOrderRequestsToCommands();
        MapFromProductRequestsToCommands();
    }

    private void MapFromRequestToQueries()
    {
        CreateMap<LoginRequest, LoginQuery>();
    }

    private void MapFromRequestToCommands()
    {
    }

    private void MapFromProductRequestsToCommands()
    {
    }

    private void MapFromOrderRequestsToCommands()
    {
    }

    private void MapFromDtoToResponse()
    {
    }
}