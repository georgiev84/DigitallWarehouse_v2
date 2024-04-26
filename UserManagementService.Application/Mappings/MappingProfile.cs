using AutoMapper;
using UserManagementService.Application.Features.Queries.LoginGoogle;
using UserManagementService.Application.Models.Dto;

namespace UserManagementService.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        MapFromQueryToDto();
        MapFromCommandToDto();
        MapFromDtoToEntity();
        MapFromEntityToDto();
        MapFromCommandToEntity();
    }

    private void MapFromQueryToDto()
    {
        CreateMap<LoginGoogleQuery, GoogleUserInfo>();
    }

    private void MapFromCommandToDto()
    {
    }

    private void MapFromDtoToEntity()
    {
    }

    private void MapFromCommandToEntity()
    {
    }

    private void MapFromEntityToDto()
    {
    }
}