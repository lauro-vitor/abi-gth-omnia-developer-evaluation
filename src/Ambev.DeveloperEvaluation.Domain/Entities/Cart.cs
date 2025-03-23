using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    public class Cart
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public virtual ICollection<CartProductItem> CartProductItems { get; set; }

        public virtual User User { get; set; }

        public Guid UserId { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdateAt { get; set; }

        public CartStatus Status { get; set; }

        public virtual ICollection<Sale> Sales { get; set; }

        public Cart()
        {
            User = new User();
            CartProductItems = [];
            Sales = [];
        }

        public void Create(DateTime date, User user)
        {
            Date = date;
            User = user;
            UserId = user.Id;
            CreatedAt = DateTime.UtcNow;
            Status = CartStatus.Open;
        }

        public void Update(DateTime date, User user)
        {
            Date = date;
            User = user;
            UserId = user.Id;
            UpdateAt = DateTime.UtcNow;
        }

        public void AddProductItem(Product product, int quantity)
        {
            if (ExistsProductInCart(product)) 
            {
                return;
            }

            var cartProductItem = new CartProductItem();

            cartProductItem.Create(Id, product, quantity);

            CartProductItems.Add(cartProductItem);
        }

        public void UpdateProductItem(Product product, int quantity)
        {
            var cartProductItemToUpdate = CartProductItems.FirstOrDefault(p => p.ProductId == product.Id);

            if (cartProductItemToUpdate != null)
            {
                cartProductItemToUpdate.Update(product, quantity);
            }
        }

        public bool ExistsProductInCart(Product product)
        {
            return CartProductItems.Any(p => p.ProductId == product.Id);
        }

        public void ClearProductItems()
        {
            CartProductItems.Clear();
        }

        public void RemoveProductItems(List<Product> products)
        {
            var productIdsToKeep = new HashSet<int>(products.Select(p => p.Id));

            var itemsToRemove = CartProductItems
                .Where(item => !productIdsToKeep.Contains(item.ProductId))
                .ToList();

            foreach (var item in itemsToRemove)
            {
                CartProductItems.Remove(item);
            }
        }

        public void SetStatusAsSaleConfirmed()
        {
            Status = CartStatus.SaleConfirmed;
            UpdateAt = DateTime.UtcNow;
        }

    }
}
