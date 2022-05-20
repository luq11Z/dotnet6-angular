using SKINET.Business.Models.Identity;

namespace SKINET.Business.Interfaces
{
    public interface ITokenService
    {
        Task<string> CreateToken(AppUser user);
    }
}
