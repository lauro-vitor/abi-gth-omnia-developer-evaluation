namespace Ambev.DeveloperEvaluation.Application.Products.GetByIdProduct;

public class GetByIdProductResult
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public decimal Price { get; set; }

    public string Description { get; set; } = string.Empty;

    public string Category { get; set; } = string.Empty;

    public string Image { get; set; } = string.Empty;

    public decimal Rate { get; set; }

    public int Count { get; set; }
}

