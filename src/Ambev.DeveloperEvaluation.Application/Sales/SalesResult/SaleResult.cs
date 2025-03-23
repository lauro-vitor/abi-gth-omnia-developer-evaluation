namespace Ambev.DeveloperEvaluation.Application.Sales.SalesResult
{
    public class SaleResult
    {
        public int Id { get; set; }
        public int SaleNumber { get; set; }
        public string Branch { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public string Status { get; set; } = string.Empty;
        public decimal Discounts { get; set; }
        public decimal TotalItemsAmount { get; set; }
        public decimal TotalSaleAmount { get; set; }
        public SaleUserResult User { get; set; } = new SaleUserResult();
        public List<SaleProductItemResult> ProductItems { get; set; } = [];
    }
}
