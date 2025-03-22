namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    public class CartProductItem
    {
        public int Id { get; set; }

        public virtual Cart Cart { get; set; }

        public int CartId { get; set; }

        public virtual Product Product { get; set; }

        public int ProductId { get; set; }

        public int Quantity { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public CartProductItem()
        {
            Cart = new Cart();
            Product = new Product();    
        }

        public void Create(int cartId, Product product, int quantity)
        {
            if (quantity <= 0)
            {
                throw new ArgumentException("Quantity must be greater than zero.", nameof(quantity));
            }

            CartId = cartId;
            Product = product;
            ProductId = product.Id;
            Quantity = quantity;
            CreatedAt = DateTime.UtcNow;
        }

        public void Update(Product product, int quantity)
        {
            if (quantity <= 0)
            {
                throw new ArgumentException("Quantity must be greater than zero.", nameof(quantity));
            }

            Product = product;
            ProductId = product.Id;
            Quantity = quantity;
            UpdatedAt = DateTime.UtcNow;
        }

    }
}
