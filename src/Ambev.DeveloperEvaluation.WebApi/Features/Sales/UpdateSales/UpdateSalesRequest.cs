namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSales
{
    public class UpdateSalesRequest
    {
        public string Branch { get; set; } = string.Empty;
        public DateTime Date { get; set; }
    }
}
