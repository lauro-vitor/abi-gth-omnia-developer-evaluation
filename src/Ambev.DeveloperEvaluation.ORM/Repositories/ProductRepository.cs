using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.QueryResult;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

public class ProductRepository : IProductRepository, IDisposable
{
    private readonly DefaultContext _context;

    public ProductRepository(DefaultContext context)
    {
        _context = context;
    }

    public async Task<Product> CreateAsync(Product product, CancellationToken cancellationToken = default)
    {
        await _context.Products.AddAsync(product, cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);

        return product;
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id, cancellationToken);

        if (product == null)
        {
            return false;
        }

        _context.Products.Remove(product);

        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }

    public IQueryable<ProductQueryResult> GetAll(string? category, string? order)
    {
        var query = _context.Products
            .AsNoTracking()
            .Select(p => new ProductQueryResult
            {
                Id = p.Id,
                Title = p.Title,
                Price = p.Price,
                Description = p.Description,
                Category = p.Category,
                Image = p.Image,
                Rating = new ProductRating
                {
                    Count = p.Count,
                    Rate = p.Rate
                }
            });

        query = FilterProducts(category, query);

        query = SortProducts(order, query);

        return query;
    }

    private static IQueryable<ProductQueryResult> FilterProducts(string? category, IQueryable<ProductQueryResult> query)
    {
        if (!string.IsNullOrWhiteSpace(category))
        {
            var startsWithWildcard = category.StartsWith('*');

            var endsWithWildcard = category.EndsWith('*');

            var cleanCategory = category.Trim('*').ToLower();

            if (startsWithWildcard && endsWithWildcard)
            {
                query = query.Where(p => p.Category.ToLower().Contains(cleanCategory));
            }
            else if (startsWithWildcard)
            {
                query = query.Where(p => p.Category.ToLower().EndsWith(cleanCategory));
            }
            else if (endsWithWildcard)
            {
                query = query.Where(p => p.Category.ToLower().StartsWith(cleanCategory));
            }
            else
            {
                query = query.Where(p => p.Category.ToLower().Equals(cleanCategory));
            }
        }

        return query;
    }

    private static IQueryable<ProductQueryResult> SortProducts(string? order, IQueryable<ProductQueryResult> query)
    {
        if (string.IsNullOrWhiteSpace(order))
        {
            return query.OrderBy(p => p.Id);
        }

        var sortClauses = order.Split(',')
            .Select(o => o.Trim())
            .Where(o => !string.IsNullOrEmpty(o))
            .ToArray();

        bool isFirst = true;

        foreach (var sortClauseItem in sortClauses)
        {
            var sortCriteria = sortClauseItem.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            if (sortCriteria.Length == 0)
            {
                continue;
            }

            var sortProperty = sortCriteria[0].Trim();

            var sortDirection = sortCriteria.ElementAtOrDefault(1)?.Trim().ToLower() ?? "asc";

            Expression<Func<ProductQueryResult, object>> sortExpression = sortProperty switch
            {
                "title" => p => p.Title,
                "price" => p => p.Price,
                "description" => p => p.Description,
                "category" => p => p.Category,
                "image" => p => p.Image,
                "rate" => p => p.Rating.Rate,
                "count" => p => p.Rating.Count,
                _ => p => p.Id,
            };

            if (isFirst)
            {
                query = sortDirection == "desc" ?
                    query.OrderByDescending(sortExpression) :
                    query.OrderBy(sortExpression);

                isFirst = false;
            }
            else
            {
                query = sortDirection == "desc" ?
                    ((IOrderedQueryable<ProductQueryResult>)query).ThenByDescending(sortExpression) :
                    ((IOrderedQueryable<ProductQueryResult>)query).ThenBy(sortExpression);
            }
        }

        return query;
    }

    public async Task<IEnumerable<string>> GetAllCategoriesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Products
            .AsNoTracking()
            .Select(p => p.Category)
            .Distinct()
            .ToListAsync(cancellationToken);
    }

    public async Task<Product?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var product = await _context.Products.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id, cancellationToken);

        return product;
    }

    public async Task<Product?> UpdateAsync(Product product, CancellationToken cancellationToken = default)
    {
        var productToUpdate = await _context.Products.FirstOrDefaultAsync(p => p.Id == product.Id, cancellationToken);

        if (productToUpdate == null)
        {
            return null;
        }

        productToUpdate.Update(product);

        _context.Products.Update(productToUpdate);

        await _context.SaveChangesAsync(cancellationToken);

        return productToUpdate;
    }

    public void Dispose()
    {
        _context?.Dispose();
        GC.SuppressFinalize(this);
    }
}

