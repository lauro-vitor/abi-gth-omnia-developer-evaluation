using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.QueryResult;

public class ProductQueryResult
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public decimal Price { get; set; }

    public string Description { get; set; } = string.Empty;

    public string Category { get; set; } = string.Empty;

    public string Image { get; set; } = string.Empty;

    public ProductRating Rating { get; set; } 

    public ProductQueryResult()
    {
        Rating = new ProductRating();
    }

    public ProductQueryResult(Product product)
    {
        Id = product.Id;
        Title = product.Title;
        Price = product.Price;
        Description = product.Description;
        Category = product.Category;
        Image = product.Image;

        Rating = new ProductRating
        {
            Count = product.Count,
            Rate = product.Rate
        };
    }
}

public class ProductRating
{
    public decimal Rate { get; set; }
    public int Count { get; set; }
}

