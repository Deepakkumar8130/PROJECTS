using System.ComponentModel.DataAnnotations;

namespace PROJECT1.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Please Enter Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please Enter Email")]
        [DataType(DataType.EmailAddress,ErrorMessage = "Please Enter Valid Mail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please Enter Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please Enter Confirm Password")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password),ErrorMessage = "Password Does Not Match With Confirm Password")]
        public string ConfirmPassword { get; set; }
    }
}
