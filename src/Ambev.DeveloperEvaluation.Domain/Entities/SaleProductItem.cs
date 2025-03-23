using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    public class SaleProductItem
    {
        public int Id { get; set; }

        public decimal UnitPrice { get; set; }

        public int Quantity { get; set; }

        public decimal TotalAmount { get; set; }

        public SaleProductItemStatus Status { get; set; }

        public int ProductId { get; set; }

        public virtual Product Product { get; set; } = new Product();

        public int SaleId { get; set; }

        public virtual Sale Sale { get; set; } = new Sale();

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public void Create(CartProductItem cartProductItem, Sale sale)
        {
            var product = cartProductItem.Product;

            CreatedAt = DateTime.UtcNow;

            Product = product;

            ProductId = product.Id;

            Sale = sale;

            SaleId = sale.Id;

            UnitPrice = product.Price;

            Quantity = cartProductItem.Quantity;

            Status = SaleProductItemStatus.Active;

            TotalAmount = UnitPrice * Quantity;
        }

        public void Activate()
        {
            Status = SaleProductItemStatus.Active;
            UpdatedAt = DateTime.UtcNow;
        }

        public void Cancel()
        {
            Status = SaleProductItemStatus.Cancelled;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
