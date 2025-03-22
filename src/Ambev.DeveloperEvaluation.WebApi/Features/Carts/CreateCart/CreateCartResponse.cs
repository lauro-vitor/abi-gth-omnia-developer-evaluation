namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.CreateCart
{
    public class CreateCartResponse
    {
        public int Id { get; set; }

        public Guid UserId { get; set; }

        public DateTime Date { get; set; }

        public ICollection<CreateCartProductItem> Products { get; set; } = [];
    }
}
