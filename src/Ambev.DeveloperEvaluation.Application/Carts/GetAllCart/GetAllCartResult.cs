using Ambev.DeveloperEvaluation.Application.Carts.GetByIdCart;

namespace Ambev.DeveloperEvaluation.Application.Carts.GetAllCart
{
    public class GetAllCartResult
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public DateTime Date { get; set; }
        public IEnumerable<GetAllCartProductItem> Products { get; set; } = [];
    }
}
