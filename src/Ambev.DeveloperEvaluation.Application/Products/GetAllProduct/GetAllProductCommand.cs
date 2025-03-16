using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.GetAllProduct;

public class GetAllProductCommand : IRequest<GetAllProductResult>
{
    public string Order { get; set; } = string.Empty;
}
