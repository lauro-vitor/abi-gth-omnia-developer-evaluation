using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.DeleteProduct;

public class DeleteProductHandler : IRequestHandler<DeleteProductCommand, DeleteProductResult>
{
    private readonly IProductRepository _productRepository;

    public DeleteProductHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<DeleteProductResult> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
    {
        var resultDelete = await _productRepository.DeleteAsync(command.Id, cancellationToken);

        var result = new DeleteProductResult();

        if (resultDelete)
            result.Message = "The product has been successfully deleted.";
        else
            result.Message = "The product deletion failed.";

        return result;
    }
}

