﻿using Microsoft.Extensions.Logging;
using Warehouse.Application.Common.Interfaces;
using Warehouse.Application.Common.Interfaces.Persistence;
using Warehouse.Application.Extensions;
using Warehouse.Application.Models.Dto;
using Warehouse.Domain.Entities;
using Warehouse.Domain.Exceptions;
using Warehouse.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;


namespace Warehouse.Infrastructure.Services;

public class ProductService : IProductService
{
    private readonly ILogger<ProductService> _logger;
    private readonly IRepository<Product> _productRepository;

    public ProductService(ILogger<ProductService> logger, IRepository<Product> productRepository)
    {
        _logger = logger;
        _productRepository = productRepository;
    }

    public async Task<ProductDto> GetFilteredProductsAsync(ItemsDto items)
    {
        try
        {
            LoggingExtensions.LogGettingProducts(_logger);

            // Fetch all products from DB
            var allProducts1 = await _productRepository.GetAllAsync();

            var allProducts = _productRepository
                .GetQueryable()
                .Include(p => p.ProductGroups)  
                .Include(p => p.OrderDetails)    
                .Include(p => p.ProductSizes)  
                .Include(p => p.Brand)
                .AsEnumerable();           // Include Brand navigation property





            if (allProducts == null)
            {
                LoggingExtensions.LogErrorFetchingProducts(_logger);
                throw new ProductNotFoundException("No products found in the database");
            }

            // Extract min and max prices
            decimal? overallMinPrice = allProducts.Min(p => p.Price);
            decimal? overallMaxPrice = allProducts.Max(p => p.Price);


            // Extract all sizes
            var allSizes = _productRepository.GetQueryable()
                        .SelectMany(p => p.ProductSizes)
                        .Select(ps => ps.Size)
                        .Distinct();
;
            //var allSizes = allProducts.SelectMany(p => p.Sizes).Distinct().ToArray();

            // Extract and split descriptions
            var wordOccurrences = allProducts.GetWordOccurrences();

            // Exctract common words
            var excludedWords = wordOccurrences.Take(5).ToList();
            var commonWords = wordOccurrences.Skip(5).Take(10).Except(excludedWords).ToArray();

            // Filter products
            LoggingExtensions.LogFilteringProducts(_logger);
            var filteredProducts = allProducts;

            filteredProducts = filteredProducts
                .FilterByMinPrice(items.MinPrice)
                .FilterByMaxPrice(items.MaxPrice)
                .FilterBySize(items.Size)
                .HighlightWords(items.Highlight);

            return new ProductDto
            {
                Filter = new ProductFilter
                {
                    MinPrice = overallMinPrice,
                    MaxPrice = overallMaxPrice,
                    AllSizes = allSizes,
                    CommonWords = commonWords
                },
                Products = filteredProducts,
            };
        }
        catch (Exception ex)
        {
            LoggingExtensions.LogErrorFetchingProducts(_logger, ex.Message);
            throw;
        }
    }
}
