using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.GetAllProduct;

public class GetAllProductHandler : IRequestHandler<GetAllProductCommand, GetAllProductResult>
{
    private readonly IProductRepository _productRepository;

    public GetAllProductHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<GetAllProductResult> Handle(GetAllProductCommand command, CancellationToken cancellationToken)
    {
        var queryProducts = _productRepository.GetAll(category: null, order: command.Order);

        var result = new GetAllProductResult
        {
            QueryProducts = queryProducts,
        };

        return await Task.FromResult(result);
    }
}

