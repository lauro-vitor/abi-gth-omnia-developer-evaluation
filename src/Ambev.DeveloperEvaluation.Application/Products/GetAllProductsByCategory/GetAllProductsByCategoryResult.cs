using Ambev.DeveloperEvaluation.Domain.QueryResult;

namespace Ambev.DeveloperEvaluation.Application.Products.GetAllProductsByCategory;

public class GetAllProductsByCategoryResult
{
    public IQueryable<ProductQueryResult> QueryProducts { get; set; }

    public GetAllProductsByCategoryResult()
    {
        QueryProducts = new List<ProductQueryResult>().AsQueryable();
    }
}

