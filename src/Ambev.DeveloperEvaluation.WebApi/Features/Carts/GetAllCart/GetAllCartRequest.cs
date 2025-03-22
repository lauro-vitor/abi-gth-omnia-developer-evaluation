using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.GetAllCart
{
    public class GetAllCartRequest
    {
        [FromQuery(Name = "_page")]
        public int PageNumber { get; set; } = 1;

        [FromQuery(Name = "_size")]
        public int PageSize { get; set; } = 10;

        [FromQuery(Name = "_order")]
        public string Order { get; set; } = string.Empty;
    }
}
