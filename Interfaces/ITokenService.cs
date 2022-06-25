
using TestApi.Entities;

namespace TestApi.Interfaces
{
    public interface ITokenService
    {
        String CreateToken(AppUser _user);
    }
}