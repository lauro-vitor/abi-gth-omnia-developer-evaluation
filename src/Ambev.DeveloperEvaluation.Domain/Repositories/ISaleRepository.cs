using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories
{
    public interface ISaleRepository
    {
        Task<Sale> CreateAsync(Sale sale, CancellationToken cancellationToken = default);

        Task<int> GetNextSaleNumberAsync(CancellationToken cancellationToken = default);

        Task<Sale?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

        Task<Sale?> GetBydProductItemAsync(int productItemId, CancellationToken cancellationToken = default);

        Task<Sale> UpdateAsync(Sale sale, CancellationToken cancellationToken = default);
    }
}
