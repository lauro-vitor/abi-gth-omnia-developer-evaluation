using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories
{
    public interface ICartRepository
    {
        Task<Cart> CreateAsync(Cart cart, CancellationToken cancellationToken = default);

        Task DeleteAsync(Cart cart, CancellationToken cancellationToken = default);

        Task<Cart?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

        Task<Cart?> GetByIdAsNoTrackingAsync(int id, CancellationToken cancellationToken = default);

        IQueryable<Cart> GetAll();

        Task<Cart> UpdateAsync(Cart cart, CancellationToken cancellationToken = default);
    }
}
