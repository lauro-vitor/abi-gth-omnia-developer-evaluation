using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    public class Sale
    {
        public int Id { get; set; }

        public string Branch { get; set; } = string.Empty;

        public int CartId { get; set; }

        public virtual Cart Cart { get; set; } = new Cart();

        public DateTime Date { get; set; }

        public decimal Discounts { get; set; }

        public int SaleNumber { get; set; }

        public SaleStatus Status { get; set; }

        public virtual ICollection<SaleProductItem> SaleProductItems { get; set; } = [];

        public decimal TotalItemsAmount { get; set; }

        public decimal TotalSaleAmount { get; set; }

        public Guid UserId { get; set; }

        public User User { get; set; } = new User();

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public void AddProductProductsItems(Cart cart)
        {
            SaleProductItems = [];

            var cartProductItems = cart.CartProductItems;

            foreach (var cartProductItem in cartProductItems)
            {
                var saleProductItem = new SaleProductItem();
                saleProductItem.Create(cartProductItem, this);
                SaleProductItems.Add(saleProductItem);
            }
        }

        public void Cancell()
        {
            if (Status == SaleStatus.Cancelled)
            {
                return;
            }

            foreach (var saleProductItem in SaleProductItems)
            {
                saleProductItem.Cancell();
            }

            Status = SaleStatus.Cancelled;
            UpdatedAt = DateTime.UtcNow;
        }

        public void CancellProductItem(int saleProductItemId)
        {
            if (Status == SaleStatus.Cancelled)
            {
                return;
            }

            var saleProductItem = SaleProductItems.FirstOrDefault(i => i.Id == saleProductItemId);

            if (saleProductItem == null)
            {
                return;
            }

            saleProductItem.Cancell();

            CalculateTotals();

            UpdatedAt = DateTime.UtcNow;
        }

        public void ActiveProductItem(int saleProductItemId)
        {
            if (Status == SaleStatus.Cancelled)
            {
                return;
            }

            var saleProductItem = SaleProductItems.FirstOrDefault(i => i.Id == saleProductItemId);

            if (saleProductItem == null)
            {
                return;
            }

            saleProductItem.Activate();

            CalculateTotals();

            UpdatedAt = DateTime.UtcNow;
        }

        public void CalculateTotals()
        {
            Discounts = 0;
            TotalItemsAmount = 0;

            var saleProductsActives = SaleProductItems.Where(i => i.Status == SaleProductItemStatus.Active);

            foreach (var saleProductActiveItem in saleProductsActives)
            {
                var discount = CalculateDiscount(saleProductActiveItem.Quantity, saleProductActiveItem.TotalAmount);

                var totalItemAmount = saleProductActiveItem.TotalAmount;

                Discounts += discount;

                TotalItemsAmount += totalItemAmount;
            }

            TotalSaleAmount = TotalItemsAmount - Discounts;
        }

        public static decimal CalculateDiscount(int quantity, decimal totalAmount)
        {
            if (quantity > 20)
            {
                throw new InvalidOperationException("Cannot sell more than 20 identical items.");
            }

            if (quantity < 4)
            {
                return 0;
            }

            decimal percentageDiscount;

            if (quantity >= 10 && quantity <= 20)
            {
                percentageDiscount = 20M;
            }
            else if (quantity >= 4 && quantity < 10)
            {
                percentageDiscount = 10M;
            }
            else
            {
                percentageDiscount = 0;
            }

            return totalAmount * (percentageDiscount / 100M);
        }

        public void Validate()
        {
            if (string.IsNullOrWhiteSpace(Branch))
                throw new ArgumentException("Branch cannot be null or empty.", nameof(Branch));

            if (Cart == null)
                throw new ArgumentNullException(nameof(Cart));

            if (User == null)
                throw new ArgumentNullException(nameof(User));

            if (SaleNumber <= 0)
                throw new ArgumentException("Sale number must be greater than zero.", nameof(SaleNumber));
        }

    }
}
