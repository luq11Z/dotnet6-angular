using AutoMapper;
using SKINET.App.Dtos;
using SKINET.Business.Models;
using SKINET.Business.Models.OrderAggregate;

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

            CreateMap<ProductCreateDto, Product>();

            CreateMap<Business.Models.Identity.Address, AddressDto>().ReverseMap();
            CreateMap<AddressDto, Business.Models.OrderAggregate.Address>();
            CreateMap<ShoppingCart, ShoppingCartDto>().ReverseMap();
            CreateMap<ShoppingCartItem, ShoppingCartItemDto>().ReverseMap();
            CreateMap<Order, OrderToReturnDto>()
                .ForMember(d => d.DeliveryMethod, o => o.MapFrom(s => s.DeliveryMethod.ShortName))
                .ForMember(d => d.ShippingPrice, o => o.MapFrom(s => s.DeliveryMethod.Price));

            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(d => d.ProductId, o => o.MapFrom(s => s.ItemOrdered.ProductItemId))
                .ForMember(d => d.ProductName, o => o.MapFrom(s => s.ItemOrdered.ProductName))
                .ForMember(d => d.PictureUrl, o => o.MapFrom(s => s.ItemOrdered.PictureUrl))
                .ForMember(d => d.PictureUrl, o => o.MapFrom<OrderItemUrlResolver>());

            CreateMap<Picture, PictureDto>()
                .ForMember(d => d.PictureUrl, o => o.MapFrom<PictureUrlResolver>());
        }
    }
}
