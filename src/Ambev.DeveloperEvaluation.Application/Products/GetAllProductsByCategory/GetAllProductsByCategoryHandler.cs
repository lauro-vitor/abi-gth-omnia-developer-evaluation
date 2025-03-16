using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.GetAllProductsByCategory;

public class GetAllProductsByCategoryHandler : IRequestHandler<GetAllProductsByCategoryCommand, GetAllProductsByCategoryResult>
{
    private readonly IProductRepository _productRepository;

    public GetAllProductsByCategoryHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<GetAllProductsByCategoryResult> Handle(GetAllProductsByCategoryCommand command, CancellationToken cancellationToken)
    {
       
        var queryProducts = _productRepository.GetAll(command.Category, command.Order);

        var result = new GetAllProductsByCategoryResult
        {
            QueryProducts = queryProducts
        };

        return await Task.FromResult(result);
    }
}

