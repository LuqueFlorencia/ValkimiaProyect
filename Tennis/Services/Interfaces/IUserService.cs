using Tennis.Models.Entity;
using Tennis.Models.Request;

namespace Tennis.Services.Interfaces
{
    public interface IUserService
    {
        Task CreateUserAsync(UserRequest userRequest);
        Task<User> ValidateUserAsync(UserRequest userRequest);
    }
}
