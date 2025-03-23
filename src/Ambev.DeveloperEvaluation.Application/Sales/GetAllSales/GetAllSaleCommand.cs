using Ambev.DeveloperEvaluation.Application.Sales.SalesResult;
using Ambev.DeveloperEvaluation.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetAllSales
{
    public class GetAllSaleCommand : IRequest<IQueryable<SaleResult>>
    {
        [FromQuery(Name = "branch")]
        public string? Branch { get; set; }

        [FromQuery(Name = "status")]
        public SaleStatus? Status { get; set; }

        [FromQuery(Name = "_minSaleNumber")]
        public int? MinSaleNumber { get; set; }

        [FromQuery(Name = "_maxSaleNumber")]
        public int? MaxSaleNumber { get; set; }

        [FromQuery(Name = "_minDate")]
        public DateTime? MinDate { get; set; }

        [FromQuery(Name = "_maxDate")]
        public DateTime? MaxDate { get; set; }

        [FromQuery(Name = "_minDiscounts")]
        public decimal? MinDiscounts { get; set; }

        [FromQuery(Name = "_maxDiscounts")]
        public decimal? MaxDiscounts { get; set; }

        [FromQuery(Name = "_minTotalItemsAmount")]
        public decimal? MinTotalItemsAmount { get; set; }

        [FromQuery(Name = "_maxTotalItemsAmount")]
        public decimal? MaxTotalItemsAmount { get; set; }

        [FromQuery(Name = "_minTotalSaleAmount")]
        public decimal? MinTotalSaleAmount { get; set; }

        [FromQuery(Name = "_maxTotalSaleAmount")]
        public decimal? MaxTotalSaleAmount { get; set; }

        [FromQuery(Name = "_order")]
        public string? Order { get; set; }
    }
}
