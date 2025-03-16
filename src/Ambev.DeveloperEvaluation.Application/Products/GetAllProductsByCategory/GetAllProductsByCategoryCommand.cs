using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.GetAllProductsByCategory;

public class GetAllProductsByCategoryCommand : IRequest<GetAllProductsByCategoryResult>
{
    public string Order { get; set; } = string.Empty;

    public string Category { get; set; } = string.Empty;
}

