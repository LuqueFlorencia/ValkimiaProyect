using Tennis.Models.Entity;
using Tennis.Models.Request;
using Tennis.Models.Response;

namespace Tennis.Mappers
{
    public static class UserMapper
    {
        public static User ToUser(this UserRequest userRequest)
        {
            return new User()
            {
                Rol = userRequest.Rol,
                UserName = userRequest.UserName,
                Password = userRequest.Password,
            };
        }
    }
}
