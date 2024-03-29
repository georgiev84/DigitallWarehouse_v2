using AutoMapper;
using Warehouse.Application.Features.Commands.BasketLines.BasketLineCreate;
using Warehouse.Application.Features.Commands.BasketLines.BasketLineUpdate;
using Warehouse.Application.Features.Commands.Orders.OrderCreate;
using Warehouse.Application.Features.Commands.Orders.OrderUpdate;
using Warehouse.Application.Features.Commands.Products.ProductCreate;
using Warehouse.Application.Features.Commands.Products.Update;
using Warehouse.Application.Features.Queries.LoginGoogle;
using Warehouse.Application.Features.Queries.Product.ProductList;
using Warehouse.Application.Models.Dto;
using Warehouse.Application.Models.Dto.BasketDtos;
using Warehouse.Application.Models.Dto.OrderDtos;
using Warehouse.Application.Models.Dto.ProductDtos;
using Warehouse.Domain.Entities.Baskets;
using Warehouse.Domain.Entities.Orders;
using Warehouse.Domain.Entities.Products;
using Warehouse.Security.Models;

namespace Warehouse.Application.Mappings;

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
        CreateMap<ProductListGetQuery, ItemsDto>();
        CreateMap<LoginGoogleQuery, GoogleUserInfo>();
    }

    private void MapFromCommandToDto()
    {
        CreateMap<ProductCreateCommand, OrderUpdateDto>();
        CreateMap<OrderCreateCommand, OrderCreateDto>();
    }

    private void MapFromDtoToEntity()
    {
        CreateMap<SizeInformationDto, ProductSize>()
            .ForMember(dest => dest.SizeId, opt => opt.MapFrom(src => src.SizeId))
            .ForMember(dest => dest.QuantityInStock, opt => opt.MapFrom(src => src.Quantity));

        CreateMap<OrderLineDto, OrderLine>();
    }

    private void MapFromCommandToEntity()
    {
        CreateMap<ProductCreateCommand, Product>()
            .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(_ => false))
            .ForMember(dest => dest.ProductSizes, opt => opt.MapFrom(src => src.Sizes))
            .ForMember(dest => dest.ProductGroups, opt => opt.MapFrom(src => src.GroupIds.Select(groupId => new ProductGroup { GroupId = groupId })));

        CreateMap<ProductUpdateCommand, Product>()
            .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(_ => false))
            .ForMember(dest => dest.ProductSizes, opt => opt.MapFrom(src => src.Sizes))
            .ForMember(dest => dest.ProductGroups, opt => opt.MapFrom(src => src.GroupIds.Select(groupId => new ProductGroup { GroupId = groupId })));

        CreateMap<OrderUpdateCommand, Order>();

        CreateMap<OrderCreateCommand, Order>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(_ => false))
            .ForMember(dest => dest.OrderLines, opt => opt.MapFrom(src => src.OrderLines));

        CreateMap<BasketLineCreateCommand, BasketLine>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.BasketId, opt => opt.MapFrom(src => src.UserId))
            .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.BasketLine.ProductId))
            .ForMember(dest => dest.SizeId, opt => opt.MapFrom(src => src.BasketLine.SizeId))
            .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.BasketLine.Quantity))
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.BasketLine.Price))
            .ForMember(dest => dest.Basket, opt => opt.Ignore())
            .ForMember(dest => dest.Product, opt => opt.Ignore())
            .ForMember(dest => dest.Size, opt => opt.Ignore());

        CreateMap<BasketLineUpdateCommand, BasketLine>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.BasketLineId));
    }

    private void MapFromEntityToDto()
    {
        CreateMap<BasketLine, BasketLineUpdateDto>()
            .ForMember(dest => dest.BasketLineId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.SizeId, opt => opt.MapFrom(src => src.SizeId))
            .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity));

        CreateMap<BasketLine, BasketLineCreateDto>();

        CreateMap<Basket, BasketDetailDto>()
             .ForMember(
                 dest => dest.Id,
                 opt => opt.MapFrom(src => src.Id))
             .ForMember(
                 dest => dest.UserId,
                 opt => opt.MapFrom(src => src.UserId))
             .ForMember(
                 dest => dest.FullName,
                 opt => opt.MapFrom(src => $"{src.User.FirstName} {src.User.LastName}"))
             .ForMember(
                 dest => dest.TotalAmount,
                 opt => opt.MapFrom(src => src.BasketLines.Sum(bl => bl.Quantity * bl.Price)));

        CreateMap<BasketLine, BasketLineDto>()
            .ForMember(
                 dest => dest.ProductName,
                 opt => opt.MapFrom(src => src.Product.Title))
             .ForMember(
                 dest => dest.SizeName,
                 opt => opt.MapFrom(src => src.Size.Name));
        CreateMap<Order, OrderWithDetailsDto>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.Name))
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.User.FirstName} {src.User.LastName}"))
            .ForMember(dest => dest.OrderLines, opt => opt.MapFrom(src => src.OrderLines));

        CreateMap<OrderLine, OrderLineDto>()
            .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
            .ForMember(dest => dest.Product, opt => opt.MapFrom(src => src.Product.Title))
            .ForMember(dest => dest.Size, opt => opt.MapFrom(src => src.Size.Name));

        CreateMap<Order, OrderCreateDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.Name))
            .ForMember(dest => dest.OrderDate, opt => opt.MapFrom(src => src.OrderDate))
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.User.FirstName} {src.User.LastName}"))
            .ForMember(dest => dest.TotalAmount, opt => opt.MapFrom(src => src.TotalAmount));

        CreateMap<Order, OrderUpdateDto>()
            .ForMember(dest => dest.PaymentId, opt => opt.MapFrom(src => src.PaymentId ?? Guid.Empty))
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId ?? Guid.Empty))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.Name))
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.User.FirstName} {src.User.LastName}"));

        CreateMap<Product, ProductDetailsDto>()
            .ForMember(dest => dest.Brand, opt => opt.MapFrom(src => src.Brand.Name))
            .ForMember(dest => dest.Groups, opt => opt.MapFrom(src => src.ProductGroups.Select(pg => new GroupDto { Id = pg.Group.Id, Name = pg.Group.Name }).ToList()))
            .ForMember(dest => dest.Sizes, opt => opt.MapFrom(src => src.ProductSizes.Select(ps => new SizeDto { Id = ps.SizeId, Quantity = ps.QuantityInStock, Name = ps.Size.Name }).ToList()));

        CreateMap<Product, ProductCreateDetailsDto>()
            .ForMember(dest => dest.Brand, opt => opt.MapFrom(src => src.Brand.Name))
            .ForMember(dest => dest.Groups, opt => opt.MapFrom(src => src.ProductGroups.Select(pg => new GroupDto { Id = pg.Group.Id, Name = pg.Group.Name }).ToList()))
            .ForMember(dest => dest.Sizes, opt => opt.MapFrom(src => src.ProductSizes.Select(ps => new SizeDto { Id = ps.SizeId, Quantity = ps.QuantityInStock, Name = ps.Size.Name }).ToList()));

        CreateMap<ProductGroup, GroupDto>();
        CreateMap<Product, ProductUpdateDetailsDto>()
            .ForMember(dest => dest.Brand, opt => opt.MapFrom(src => src.Brand.Name))
            .ForMember(dest => dest.Groups, opt => opt.MapFrom(src => src.ProductGroups.Select(pg => new GroupDto { Id = pg.Group.Id, Name = pg.Group.Name }).ToList()))
            .ForMember(dest => dest.Sizes, opt => opt.MapFrom(src => src.ProductSizes.Select(ps => new SizeDto { Id = ps.SizeId, Quantity = ps.QuantityInStock, Name = ps.Size.Name }).ToList()));

        CreateMap<Order, OrderDto>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.Name))
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.User.FirstName} {src.User.LastName}"));
    }
}