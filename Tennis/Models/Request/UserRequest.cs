using System.ComponentModel.DataAnnotations;

namespace Tennis.Models.Request
{
    public class UserRequest
    {
        public string Rol { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
