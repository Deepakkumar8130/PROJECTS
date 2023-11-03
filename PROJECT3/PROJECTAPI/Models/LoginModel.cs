using System.ComponentModel.DataAnnotations;

namespace PROJECTAPI.Models
{
    public class LoginModel
    {
        [Required]
        public string UserId { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
