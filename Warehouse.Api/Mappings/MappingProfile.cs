using AutoMapper;
using Warehouse.Api.Models.OrderResponses.Orders;
using Warehouse.Api.Models.Requests;
using Warehouse.Api.Models.Requests.Basket;
using Warehouse.Api.Models.Requests.BasketLine;
using Warehouse.Api.Models.Requests.Orders;
using Warehouse.Api.Models.Requests.Product;
using Warehouse.Api.Models.Responses.BasketResponses;
using Warehouse.Api.Models.Responses.LoginResponses;
using Warehouse.Api.Models.Responses.OrderResponses;
using Warehouse.Api.Models.Responses.ProductResponses;
using Warehouse.Application.Features.Commands.BasketLines.BasketLineBulkDelete;
using Warehouse.Application.Features.Commands.BasketLines.BasketLineCreate;
using Warehouse.Application.Features.Commands.BasketLines.BasketLineDelete;
using Warehouse.Application.Features.Commands.BasketLines.BasketLineUpdate;
using Warehouse.Application.Features.Commands.Orders.OrderCreate;
using Warehouse.Application.Features.Commands.Orders.OrderDelete;
using Warehouse.Application.Features.Commands.Orders.OrderUpdate;
using Warehouse.Application.Features.Commands.Products.ProductCreate;
using Warehouse.Application.Features.Commands.Products.Update;
using Warehouse.Application.Features.Queries.Basket.BasketSingleQuery;
using Warehouse.Application.Features.Queries.Orders.OrderGetAll;
using Warehouse.Application.Features.Queries.Orders.OrderGetSingle;
using Warehouse.Application.Features.Queries.Product.ProductList;
using Warehouse.Application.Models.Dto;
using Warehouse.Application.Models.Dto.BasketDtos;
using Warehouse.Application.Models.Dto.OrderDtos;
using Warehouse.Application.Models.Dto.ProductDtos;
using Warehouse.Application.Models.Login;

namespace Warehouse.Api.Mappings;

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
        CreateMap<ProductFilterRequest, ProductListGetQuery>();
        CreateMap<OrderRequest, OrderGetAllQuery>();
        CreateMap<OrderSingleRequest, OrderGetSingleQuery>();
        CreateMap<BasketSingleRequest, BasketSingleQuery>();
    }

    private void MapFromRequestToCommands()
    {
        CreateMap<BasketLineCreateRequest, BasketLineCreateCommand>();
        CreateMap<BasketLineUpdateRequest, BasketLineUpdateCommand>();
        CreateMap<BasketLineDeleteRequest, BasketLineDeleteCommand>();
        CreateMap<BasketLineBulkDeleteRequest, BasketLineBulkDeleteCommand>();
    }

    private void MapFromProductRequestsToCommands()
    {
        CreateMap<ProductCreateRequest, ProductCreateCommand>();
        CreateMap<ProductUpdateRequest, ProductUpdateCommand>();
    }

    private void MapFromOrderRequestsToCommands()
    {
        CreateMap<OrderCreateRequest, OrderCreateCommand>();
        CreateMap<OrderLineRequest, OrderLineDto>();
        CreateMap<OrderUpdateRequest, OrderUpdateCommand>();
        CreateMap<OrderSingleRequest, OrderGetSingleQuery>();
        CreateMap<OrderDeleteRequest, OrderDeleteCommand>();
    }

    private void MapFromDtoToResponse()
    {
        CreateMap<ProductDto, ProductDetailedResponse>();
        CreateMap<SizeInformationRequest, SizeInformationDto>()
            .ForMember(dest => dest.SizeId, opt => opt.MapFrom(src => src.Id));
        CreateMap<SizeDto, SizeResponse>();
        CreateMap<OrderUpdateDto, ProductCreateResponse>();
        CreateMap<ProductUpdateDetailsDto, ProductUpdateResponse>();
        CreateMap<OrderDto, OrderResponse>();
        CreateMap<OrderCreateDto, OrderCreateResponse>();
        CreateMap<OrderUpdateDto, OrderUpdateResponse>();
        CreateMap<OrderLineDto, OrderLineResponse>();
        CreateMap<OrderWithDetailsDto, OrderDetailedResponse>();
        CreateMap<OrderUpdateDto, OrderUpdateResponse>();
        CreateMap<OrderUpdateDto, OrderDetailedResponse>();
        CreateMap<BasketCreateDto, BasketCreateResponse>();
        CreateMap<BasketLineCreateDto, BasketLineResponse>();
        CreateMap<BasketLineCreateDto, BasketLineDto>();
        CreateMap<BasketDetailDto, BasketResponse>();
        CreateMap<BasketLineUpdateDto, BasketLineUpdateResponse>();
        CreateMap<OrderUpdateDto, OrderResponse>();
        CreateMap<ProductCreateDetailsDto, ProductCreateResponse>();
        CreateMap<LoginModel, LoginResponse>();
        CreateMap<RefreshModel, RefreshTokenResponse>();
    }
}