using AutoMapper;
using SKINET.App.Dtos;
using SKINET.Business.Models;
using SKINET.Business.Models.Identity;

namespace SKINET.App.Configuration
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<Product, ProductDTO>()
                .ForMember(d => d.ProductBrand, o => o.MapFrom(d => d.ProductBrand.Name))
                .ForMember(d => d.ProductType, o => o.MapFrom(d => d.ProductType.Name))
                .ForMember(d => d.CreatedAt, o => o.MapFrom(d => d.CreatedAt.ToLocalTime()))
                .ForMember(d => d.PictureUrl, o => o.MapFrom<ProductUrlResolver>());

            CreateMap<Address, AddressDto>().ReverseMap();
        }
    }
}
