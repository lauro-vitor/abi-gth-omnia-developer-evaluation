namespace Ambev.DeveloperEvaluation.Application.Carts.CreateCart
{
    public class CreateCartResult
    {
        public int Id { get; set; }

        public Guid UserId { get; set; }

        public DateTime Date { get; set; }

        public ICollection<CreateCartProductItem> Products { get; set; } = [];
    }
}
