using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Tennis.Mappers;
using Tennis.Models.Entity;
using Tennis.Models.Request;
using Tennis.Repository;
using Tennis.Services.Encryption;
using Tennis.Services.Interfaces;

namespace Tennis.Services
{
    public class UserService : IUserService
    {
        private readonly TennisContext _context;
        private readonly IEncryptionService _encryptionService;
        public UserService(TennisContext context, IEncryptionService encryptionService)
        {
            _context = context;
            _encryptionService = encryptionService;
        }

        public async Task CreateUserAsync(UserRequest userRequest)
        {
            var user = await _context.Set<User>()
                .FirstOrDefaultAsync(u => u.UserName.Equals(userRequest.UserName));
            if (user != null)
            {
                throw new Exception($"The user {userRequest.UserName} has already exist.");
            }
            var newUser = new User();
            newUser = userRequest.ToUser();
            newUser.Password = _encryptionService.Encrypt(userRequest.Password);

            _context.Add(newUser);
            await _context.SaveChangesAsync();
        }

        public async Task<User> ValidateUserAsync(UserRequest userRequest)
        {
            var passEcrypted = _encryptionService.Decrypt(userRequest.Password);
            var user = await _context.Set<User>()
                .FirstOrDefaultAsync(u => u.UserName.Equals(userRequest.UserName) && 
                u.Password.Equals(passEcrypted));

            if (user == null)
            {
                throw new Exception("Invalid Credentials");
            }

            return user;
        }
    }
}
