using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;
using System.Linq.Expressions;

namespace Ambev.DeveloperEvaluation.Application.Carts.GetAllCart
{
    public class GetAllCartHandler : IRequestHandler<GetAllCartCommand, IQueryable<GetAllCartResult>>
    {
        private readonly ICartRepository _cartRepository;

        public GetAllCartHandler(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public Task<IQueryable<GetAllCartResult>> Handle(GetAllCartCommand command, CancellationToken cancellationToken)
        {
            var query = _cartRepository.GetAll();

            query = SortCarts(command.Order, query);

            var queryResult = query.Select(c => new GetAllCartResult
            {
                Id = c.Id,
                Date = c.Date,
                UserId = c.UserId,
                Products = c.CartProductItems.Select(i => new GetAllCartProductItem 
                { 
                    ProductId = i.ProductId,
                    Quantity = i.Quantity
                })
            });

            return Task.FromResult(queryResult);
        }

        private static IQueryable<Cart> SortCarts(string? order, IQueryable<Cart> query)
        {
            if (string.IsNullOrWhiteSpace(order))
            {
                return query.OrderBy(p => p.CreatedAt);
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

                Expression<Func<Cart, object>> sortExpression = sortProperty switch
                {
                    "id" => c => c.Id,
                    "date" => c => c.Date,
                    "userid" => c => c.UserId,
                    _ => c => c.CreatedAt,
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
                        ((IOrderedQueryable<Cart>)query).ThenByDescending(sortExpression) :
                        ((IOrderedQueryable<Cart>)query).ThenBy(sortExpression);
                }
            }

            return query;
        }
    }
}
