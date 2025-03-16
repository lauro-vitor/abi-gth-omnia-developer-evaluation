﻿using Ambev.DeveloperEvaluation.Domain.QueryResult;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.GetByIdProduct;

public class GetByIdProductResponse
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public decimal Price { get; set; }

    public string Description { get; set; } = string.Empty;

    public string Category { get; set; } = string.Empty;

    public string Image { get; set; } = string.Empty;

    public ProductRating Rating { get; set; } = new ProductRating();
}
