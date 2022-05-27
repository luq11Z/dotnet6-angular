using AutoMapper;
using SKINET.App.Dtos;
using SKINET.Business.Models;

namespace SKINET.App.Configuration
{
    public class ProductUrlResolver : IValueResolver<Product, ProductDTO, string>
    {
        private readonly IConfiguration _configuration;

        public ProductUrlResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /* This method will get the url from app.settings and set the full url address to the picture (product). */
        public string Resolve(Product source, ProductDTO destination, string destMember, ResolutionContext context)
        {
            var photo = source.Pictures.FirstOrDefault(p => p.IsMain);

            if (photo != null)
            {
                return _configuration["ApiUrl"] + photo.PictureUrl;
            }

            return _configuration["ApiUrl"] + "images/products/placeholder.png";
        }
    }
}
