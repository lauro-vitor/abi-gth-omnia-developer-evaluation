using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

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

        public IQueryable<Sale> GetAll()
        {
            return _context.Sales
                .AsNoTracking()
                .Include(s => s.User)
                .Include(s => s.SaleProductItems).ThenInclude(i => i.Product);
        }

        public async Task<int> GetNextSaleNumberAsync(CancellationToken cancellationToken = default)
        {
            var maxSaleNumber = await _context.Sales
                .AsNoTracking()
                .MaxAsync(s => s.SaleNumber, cancellationToken);

            if (maxSaleNumber <= 0)
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

        public async Task<Sale> UpdateAsync(Sale sale, CancellationToken cancellationToken = default)
        {
            _context.Sales.Update(sale);
            await _context.SaveChangesAsync(cancellationToken);
            return sale;
        }

        public Task<Sale?> GetBydProductItemAsync(int productItemId, CancellationToken cancellationToken = default)
        {
            Expression<Func<Sale, bool>> filterExpression = s => s.SaleProductItems.Any(i => i.Id == productItemId);

            return _context.Sales
                .Include(s => s.User)
                .Include(s => s.SaleProductItems).ThenInclude(i => i.Product)
                .FirstOrDefaultAsync(filterExpression, cancellationToken);
        }

        public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            var sale = await _context.Sales.Include(s => s.SaleProductItems).FirstOrDefaultAsync(s => s.Id == id, cancellationToken);

            if (sale == null)
            {
                return false;
            }

            _context.SaleProductItems.RemoveRange(sale.SaleProductItems);

            _context.Sales.Remove(sale);

            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }

        public void Dispose()
        {
            _context?.Dispose();
            GC.SuppressFinalize(this);
        }


    }
}
