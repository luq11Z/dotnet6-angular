using SKINET.Business.Models.Identity;

namespace SKINET.Business.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }
}
