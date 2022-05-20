using AutoMapper;
using SKINET.App.Dtos;
using SKINET.Business.Models;

namespace SKINET.App.Configuration
{
    public class PictureUrlResolver : IValueResolver<Picture, PictureDto, string>
    {
        private readonly IConfiguration _configuration;

        public PictureUrlResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string Resolve(Picture source, PictureDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.PictureUrl))
            {
                return _configuration["ApiUrl"] + source.PictureUrl;
            }

            return null;
        }
    }
}
