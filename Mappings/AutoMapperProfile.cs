using AutoMapper;
using LemonHiveEcommerce.DTOs;
using LemonHiveEcommerce.Models;

namespace LemonHiveEcommerce.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<CartItem, CartItemDto>().ReverseMap();
        }
    }
}
