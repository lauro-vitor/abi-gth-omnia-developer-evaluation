namespace Ambev.DeveloperEvaluation.Application.Carts.UpdateCart
{
    public class UpdateCartResult
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public DateTime Date { get; set; }
        public ICollection<UpdateCartProductItem> Products { get; set; } = [];
    }
}
