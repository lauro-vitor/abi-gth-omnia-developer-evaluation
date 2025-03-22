using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Carts.GetByIdCart
{
    public class GetByIdCartCommand : IRequest<GetByIdCartResult>
    {
        public int  Id { get; set; }
    }
}
