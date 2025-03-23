using Ambev.DeveloperEvaluation.Application.Sales.SalesResult;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using System.Linq.Expressions;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetAllSales
{
    public class GetAllSaleHandler : IRequestHandler<GetAllSaleCommand, IQueryable<SaleResult>>
    {
        private readonly IMapper _mapper;
        private readonly ISaleRepository _saleRepository;

        public GetAllSaleHandler(IMapper mapper, ISaleRepository saleRepository)
        {
            _mapper = mapper;
            _saleRepository = saleRepository;
        }

        public Task<IQueryable<SaleResult>> Handle(GetAllSaleCommand command, CancellationToken cancellationToken)
        {
            var query = _saleRepository.GetAll();

            query = FilterBranch(query, command.Branch);
            query = FilterStatus(query, command.Status);
            query = FilterSaleNumber(query, command.MinSaleNumber, command.MaxSaleNumber);
            query = FilterDate(query, command.MinDate, command.MaxDate);
            query = FilterDiscounts(query, command.MinDiscounts, command.MaxDiscounts);
            query = FilterTotalItemsAmount(query, command.MinTotalItemsAmount, command.MaxTotalItemsAmount);
            query = FilterTotalSaleAmount(query, command.MinTotalSaleAmount, command.MaxTotalSaleAmount);

            query = SortSales(query, command.Order);

            var result = query.ProjectTo<SaleResult>(_mapper.ConfigurationProvider);

            return Task.FromResult(result);
        }

        private static IQueryable<Sale> FilterBranch(IQueryable<Sale> query, string? branch)
        {
            if (string.IsNullOrWhiteSpace(branch))
            {
                return query;
            }

            var startsWithWildcard = branch.StartsWith('*');
            var endsWithWildcard = branch.EndsWith('*');
            var cleanValue = branch.Trim('*').ToLower();

            if (startsWithWildcard && endsWithWildcard)
            {
                query = query.Where(p => p.Branch.ToLower().Contains(cleanValue));
            }
            else if (startsWithWildcard)
            {
                query = query.Where(p => p.Branch.ToLower().EndsWith(cleanValue));
            }
            else if (endsWithWildcard)
            {
                query = query.Where(p => p.Branch.ToLower().StartsWith(cleanValue));
            }
            else
            {
                query = query.Where(p => p.Branch.ToLower().Equals(cleanValue));
            }

            return query;
        }

        private static IQueryable<Sale> FilterStatus(IQueryable<Sale> query, SaleStatus? status)
        {
            if (!status.HasValue)
            {
                return query;
            }

            query = query.Where(s => s.Status == status);

            return query;
        }

        private static IQueryable<Sale> FilterSaleNumber(IQueryable<Sale> query, int? minSaleNumber, int? maxSaleNumber)
        {
            if (minSaleNumber.HasValue)
            {
                query = query.Where(s => s.SaleNumber >= minSaleNumber.Value);
            }

            if (maxSaleNumber.HasValue)
            {
                query = query.Where(s => s.SaleNumber <= maxSaleNumber.Value);
            }

            return query;
        }

        private static IQueryable<Sale> FilterDate(IQueryable<Sale> query, DateTime? minDate, DateTime? maxDate)
        {
            if (minDate.HasValue)
            {
                query = query.Where(s => s.Date >= minDate.Value.ToUniversalTime());
            }

            if (maxDate.HasValue)
            {
                query = query.Where(s => s.Date <= maxDate.Value.ToUniversalTime());
            }

            return query;
        }

        private static IQueryable<Sale> FilterDiscounts(IQueryable<Sale> query, decimal? minDiscounts, decimal? maxDiscounts)
        {
            if (minDiscounts.HasValue)
            {
                query = query.Where(s => s.Discounts >= minDiscounts.Value);
            }

            if (maxDiscounts.HasValue)
            {
                query = query.Where(s => s.Discounts <= maxDiscounts.Value);
            }

            return query;
        }

        private static IQueryable<Sale> FilterTotalItemsAmount(IQueryable<Sale> query, decimal? minTotalItemsAmount, decimal? maxTotalItemsAmount)
        {
            if (minTotalItemsAmount.HasValue)
            {
                query = query.Where(s => s.TotalItemsAmount >= minTotalItemsAmount.Value);
            }

            if (maxTotalItemsAmount.HasValue)
            {
                query = query.Where(s => s.TotalItemsAmount <= maxTotalItemsAmount.Value);
            }

            return query;
        }

        private static IQueryable<Sale> FilterTotalSaleAmount(IQueryable<Sale> query, decimal? minTotalSaleAmount, decimal? maxTotalSaleAmount)
        {
            if (minTotalSaleAmount.HasValue)
            {
                query = query.Where(s => s.TotalSaleAmount >= minTotalSaleAmount.Value);
            }

            if (maxTotalSaleAmount.HasValue)
            {
                query = query.Where(s => s.TotalSaleAmount <= maxTotalSaleAmount.Value);
            }

            return query;
        }

        private static IQueryable<Sale> SortSales(IQueryable<Sale> query, string? order)
        {
            if (string.IsNullOrWhiteSpace(order))
            {
                return query.OrderBy(s => s.CreatedAt);
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

                Expression<Func<Sale, object>> sortExpression = sortProperty switch
                {
                    "id" => s => s.Id,
                    "saleNumber" => s => s.SaleNumber,
                    "branch" => s => s.Branch,
                    "date" => s => s.Date,
                    "status" => s => s.Status,
                    "discounts" => s => s.Discounts,
                    "totalItemsAmount" => s => s.TotalItemsAmount,
                    "totalSaleAmount" => s => s.TotalSaleAmount,
                    _ => s => s.Id,
                };

                if (isFirst)
                {
                    query = sortDirection == "desc"
                        ? query.OrderByDescending(sortExpression)
                        : query.OrderBy(sortExpression);

                    isFirst = false;
                }
                else
                {
                    query = sortDirection == "desc"
                        ? ((IOrderedQueryable<Sale>)query).ThenByDescending(sortExpression)
                        : ((IOrderedQueryable<Sale>)query).ThenBy(sortExpression);
                }
            }

            return query;
        }

    }

}
