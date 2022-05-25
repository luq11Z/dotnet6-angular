using Microsoft.AspNetCore.Http;
using SKINET.Business.Models;

namespace SKINET.Business.Interfaces.IServices
{
    public interface IPictureService
    {
        Task<Picture> SaveToDiskAsync(IFormFile file);
        void DeleteFromDisk(Picture picture);
    }
}
