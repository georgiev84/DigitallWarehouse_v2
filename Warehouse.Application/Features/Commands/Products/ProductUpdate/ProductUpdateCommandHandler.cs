using AutoMapper;
using MediatR;
using Warehouse.Application.Common.Interfaces.Persistence;
using Warehouse.Application.Models.Dto.ProductDtos;
using Warehouse.Domain.Entities.Products;
using Warehouse.Domain.Exceptions;

namespace Warehouse.Application.Features.Commands.Products.Update;

public class ProductUpdateCommandHandler(IMapper _mapper, IUnitOfWork _unitOfWork) : IRequestHandler<ProductUpdateCommand, ProductUpdateDetailsDto>
{
    public async Task<ProductUpdateDetailsDto> Handle(ProductUpdateCommand command, CancellationToken cancellationToken)
    {
        var existingProduct = await _unitOfWork.Products.GetProductDetailsByIdAsync(command.Id);

        if (existingProduct is null)
        {
            throw new ProductNotFoundException($"Product with ID {command.Id} not found.");
        }

        _mapper.Map(command, existingProduct);

        existingProduct.ProductSizes.Clear();
        foreach (var newSize in command.Sizes)
        {
            existingProduct.ProductSizes.Add(_mapper.Map<ProductSize>(newSize));
        }

        existingProduct.ProductGroups.Clear();
        foreach (var groupId in command.GroupIds)
        {
            existingProduct.ProductGroups.Add(new ProductGroup
            {
                GroupId = groupId
            });
        }


        _unitOfWork.Products.Update(existingProduct);
        await _unitOfWork.SaveAsync();

        var updatedProductDto = _mapper.Map<ProductUpdateDetailsDto>(existingProduct);

        return updatedProductDto;
    }
}