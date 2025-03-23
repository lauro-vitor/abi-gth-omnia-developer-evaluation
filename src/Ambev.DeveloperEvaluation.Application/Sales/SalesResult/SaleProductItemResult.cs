namespace Ambev.DeveloperEvaluation.Application.Sales.SalesResult
{
    public class SaleProductItemResult
    {
        public int Id { get; set; }
        public string Status { get; set; } = string.Empty;
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public decimal TotalAmount { get; set; }
        public string ProductName { get; set; } = string.Empty;
    }
}
