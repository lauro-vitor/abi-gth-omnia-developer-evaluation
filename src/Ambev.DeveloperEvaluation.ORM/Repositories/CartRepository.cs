using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories
{
    public class CartRepository : ICartRepository, IDisposable
    {
        private readonly DefaultContext _context;

        public CartRepository(DefaultContext context)
        {
            _context = context;
        }

        public async Task<Cart> CreateAsync(Cart cart, CancellationToken cancellationToken = default)
        {
            await _context.AddAsync(cart, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return cart;
        }

        public async Task DeleteAsync(Cart cart, CancellationToken cancellationToken = default)
        {
            _context.Remove(cart);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public IQueryable<Cart> GetAll()
        {
            return _context.Carts.AsNoTracking();
        }

        public async Task<Cart?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _context.Carts
                .Include(c => c.User)
                .Include(c => c.CartProductItems)
                .ThenInclude(c => c.Product)
                .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
        }

        public async Task<Cart?> GetByIdAsNoTrackingAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _context.Carts
                .Include(c => c.CartProductItems)
                .ThenInclude(c => c.Product)
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
        }

        public async Task<Cart> UpdateAsync(Cart cart, CancellationToken cancellationToken = default)
        {
            _context.Update(cart);
            await _context.SaveChangesAsync(cancellationToken);
            return cart;
        }

        public void Dispose()
        {
            _context?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
