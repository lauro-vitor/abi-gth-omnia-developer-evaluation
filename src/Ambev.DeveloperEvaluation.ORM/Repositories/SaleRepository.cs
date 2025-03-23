using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories
{
    public class SaleRepository : ISaleRepository, IDisposable
    {
        private readonly DefaultContext _context;

        public SaleRepository(DefaultContext context)
        {
            _context = context;
        }

        public async Task<Sale> CreateAsync(Sale sale, CancellationToken cancellationToken = default)
        {
            await _context.Sales.AddAsync(sale, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return sale;
        }

        public async Task<int> GetNextSaleNumberAsync(CancellationToken cancellationToken = default)
        {
            var maxSaleNumber = await _context.Sales
                .AsNoTracking()
                .MaxAsync(s => s.SaleNumber, cancellationToken);

            if(maxSaleNumber <= 0)
            {
                return 1;
            }

            return maxSaleNumber + 1;
        }

        public async Task<Sale?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _context.Sales
                  .Include(s => s.User)
                  .Include(s => s.SaleProductItems).ThenInclude(i => i.Product)
                  .FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
        }

        public void Dispose()
        {
            _context?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
