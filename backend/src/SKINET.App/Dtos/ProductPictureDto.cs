using SKINET.App.Extensions;

namespace SKINET.App.Dtos
{
    public class ProductPictureDto
    {
        [MaxFileSize(2 * 1024 * 1024)]
        [AllowedExtensions(new[] {".jpg", ".png", ".jpeg"})]
        public IFormFile Picture { get; set; }
    }
}
