using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.GetAllCategories;

public class GetAllCategoriesHandler : IRequestHandler<GetAllCategoriesCommand, GetAllCategoriesResult>
{
    private readonly IProductRepository _productRepository;

    public GetAllCategoriesHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<GetAllCategoriesResult> Handle(GetAllCategoriesCommand request, CancellationToken cancellationToken)
    {
        var result = new GetAllCategoriesResult()
        {
            Categories = await _productRepository.GetAllCategoriesAsync(cancellationToken)
        };

        return result;
    }
}

