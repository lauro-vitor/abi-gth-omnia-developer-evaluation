using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.GetByIdProduct;

public class GetByIdProductCommand : IRequest<GetByIdProductResult>
{
    public int Id { get; set; }
}

