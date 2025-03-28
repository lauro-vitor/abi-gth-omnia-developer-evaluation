﻿using Ambev.DeveloperEvaluation.Application.Carts.GetAllCart;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.GetAllCart
{
    public class GetAllCartProfile : Profile
    {
        public GetAllCartProfile()
        {
            CreateMap<GetAllCartRequest, GetAllCartCommand>();
        }
    }
}
