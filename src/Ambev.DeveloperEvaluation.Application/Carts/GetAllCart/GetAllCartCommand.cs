using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Carts.GetAllCart
{
    public class GetAllCartCommand : IRequest<IQueryable<GetAllCartResult>>
    {
        public string Order { get; set; } = string.Empty;
    }
}
