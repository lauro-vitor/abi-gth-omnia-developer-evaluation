using Ambev.DeveloperEvaluation.Domain.QueryResult;

namespace Ambev.DeveloperEvaluation.Application.Products.GetAllProduct;

public class GetAllProductResult
{
    public IQueryable<ProductQueryResult> QueryProducts { get; set; }

    public GetAllProductResult()
    {
        QueryProducts = new List<ProductQueryResult>().AsQueryable();
    }
}
