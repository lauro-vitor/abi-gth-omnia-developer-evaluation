using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Carts.UpdateCart
{

    public class UpdateCartCommand : IRequest<UpdateCartResult>
    {
        public int CartId { get; set; }
        public Guid UserId { get; set; }
        public DateTime Date { get; set; }
        public ICollection<UpdateCartProductItem> Products { get; set; } = [];
    }
}
