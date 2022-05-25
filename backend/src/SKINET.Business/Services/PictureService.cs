using Microsoft.AspNetCore.Http;
using SKINET.Business.Interfaces.IServices;
using SKINET.Business.Models;

namespace SKINET.Business.Services
{
    public class PictureService : IPictureService
    {
        public async Task<Picture> SaveToDiskAsync(IFormFile file)
        {
            var picture = new Picture();

            if (file.Length > 0)
            {
                var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
                var filePath = Path.Combine("wwwroot/images/products", fileName);
                await using var fileStream = new FileStream(filePath, FileMode.Create);
                await file.CopyToAsync(fileStream);

                picture.FileName = fileName;
                picture.PictureUrl = "images/products/" + fileName;

                return picture;
            }

            return null;
        }

        public void DeleteFromDisk(Picture picture)
        {
            if (File.Exists(Path.Combine("wwwroot/images/products", picture.FileName)))
            {
                File.Delete("wwwroot/images/products/" + picture.FileName);
            }
        }
    }
}
