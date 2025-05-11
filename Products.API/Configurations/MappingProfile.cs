using AutoMapper;
using Products.Domain.Entities;
using Products.API.Models;

namespace Products.API.MappingProfiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductDto>();

            CreateMap<CreateOrUpdateProductDto, Product>();
        }
    }
}
