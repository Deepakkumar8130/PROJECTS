using System.ComponentModel.DataAnnotations;

namespace PROJECT6.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Please Enter Email")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Please Enter Valid Mail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please Enter Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
