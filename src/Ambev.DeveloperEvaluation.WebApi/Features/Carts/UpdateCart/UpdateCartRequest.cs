namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.UpdateCart
{
    public class UpdateCartRequest
    {
        public Guid UserId { get; set; }
        public DateTime Date { get; set; }
        public ICollection<UpdateCartProductItem> Products { get; set; } = [];
    }
}
