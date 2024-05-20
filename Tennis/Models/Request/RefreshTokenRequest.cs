using System.ComponentModel.DataAnnotations;

namespace Tennis.Models.Request
{
    public class RefreshTokenRequest
    {
        [Required]
        public string RefreshToken { get; set; }

    }
}
