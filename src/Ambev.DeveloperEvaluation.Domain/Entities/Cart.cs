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

        public Cart()
        {
            CartProductItems = [];
            User = new User();
        }

        public void AddProductItem(int cartId, Product product, int quantity)
        {
            var cartProductItem = new CartProductItem();

            cartProductItem.Create(cartId, product, quantity);

            CartProductItems.Add(cartProductItem);
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

    }
}
