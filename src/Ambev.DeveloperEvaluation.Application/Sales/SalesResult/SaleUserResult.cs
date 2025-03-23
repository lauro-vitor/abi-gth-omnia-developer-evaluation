namespace Ambev.DeveloperEvaluation.Application.Sales.SalesResult
{
    public class SaleUserResult
    {
        public Guid Id { get; set; }
        public string Phone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public SaleUserNameResult Name { get; set; } = new SaleUserNameResult();
        public SaleUserAddressResult Address { get; set; } = new SaleUserAddressResult();
    }
}
