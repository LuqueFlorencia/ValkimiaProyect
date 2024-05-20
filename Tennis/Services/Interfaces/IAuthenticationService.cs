using Tennis.Models.Entity;
using Tennis.Models.Response;

namespace Tennis.Services.Interfaces
{
    public interface IAuthenticationService
    {
        TokenResponse GenerateToken(User user);
        bool ValidateRefreshToken(User user);
    }
}
