using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Tennis.Configuration;
using Tennis.Models.Entity;
using Tennis.Models.Response;
using Tennis.Repository;
using Tennis.Services.Interfaces;

namespace Tennis.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly AuthenticationOptions _authenticationOptions;
        private readonly TennisContext _context;

        public AuthenticationService(AuthenticationOptions authenticationOptions, TennisContext context)
        {
            _authenticationOptions = authenticationOptions;
            _context = context;
        }

        public TokenResponse GenerateToken(User user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            var claims = new List<Claim>()
            {
                new Claim("Id", user.IdUser.ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName),
                new Claim(JwtRegisteredClaimNames.GivenName, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationOptions.Key));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expDate = DateTime.UtcNow.AddMinutes(_authenticationOptions.Expiration);

            var expRefrestokenDate = DateTime.UtcNow.AddMinutes(_authenticationOptions.RefreshTokenExpiration);

            var token = new JwtSecurityToken(
                issuer: _authenticationOptions.Issuer,
                audience: _authenticationOptions.Audience,
                claims: claims,
                expires: expDate,
                signingCredentials: credentials
            );

            return new TokenResponse
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                ExpirationToken = expDate,
                RefreshToken = GenerateRefreshToken(),
                RefreshTokenExpiration = expRefrestokenDate
            };
        }

        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        public bool ValidateRefreshToken(User user)
        {
            return user.RefreshTokenExpiration > DateTime.UtcNow;
        }

        public async Task UpdateRefreshToken(User userFromDB, string refreshToken)
        {
            userFromDB.RefreshToken = refreshToken;
            userFromDB.RefreshTokenExpiration = DateTime.UtcNow.AddMinutes(_authenticationOptions.RefreshTokenExpiration);

            _context.Update(userFromDB);
            await _context.SaveChangesAsync();
        }
    }
}
