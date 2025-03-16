using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories;

/// <summary>
/// Repository interface for Product entity operations.
/// </summary>
public interface IProductRepository
{
    /// <summary>
    /// Creates a new product in the repository.
    /// </summary>
    /// <param name="product">The product to be created.</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created product.</returns>
    Task<Product> CreateAsync(Product product, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates an existing product in the repository.
    /// </summary>
    /// <param name="product">The product to be updated.</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The updated product; otherwise, null.</returns>
    Task<Product?> UpdateAsync(Product product, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a product by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the product to retrieve.</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The product if found; otherwise, null.</returns>
    Task<Product?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a paginated list of products, optionally filtered by category and ordered by a specified field.
    /// </summary>
    /// <param name="page">The page number to retrieve (default is 1).</param>
    /// <param name="size">The number of products per page (default is 10).</param>
    /// <param name="order">The field by which to order the products (e.g., "price desc, title asc") (optional).</param>
    /// <param name="category">The category to filter the products by (optional).</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>A list of products.</returns>
    Task<IEnumerable<Product>> GetAllAsync(
        int page = 1,
        int size = 10,
        string? order = null,
        string? category = null,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Retrieves all available product categories.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>A list of category names.</returns>
    Task<IEnumerable<string>> GetAllCategoriesAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes a product by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the product to delete.</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if the product was deleted; otherwise, false.</returns>
    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
}
