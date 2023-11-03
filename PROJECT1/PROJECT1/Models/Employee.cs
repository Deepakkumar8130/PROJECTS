using System.ComponentModel.DataAnnotations;

namespace PROJECT1.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.EmailAddress,ErrorMessage = "Please Enter Valid Input")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Please Enter Valid Number")]
        [StringLength(10, ErrorMessage = "Please Enter Valid Number")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
        public string Mobile { get; set; }

        [Required]
        public string Gender { get; set; }

        [Required]
        [Range(1,20,ErrorMessage = "please select valid country")]
        public int country_id { get; set; }

        [Required]
        [Range(1, 20, ErrorMessage = "please select valid state")]
        public int state_id { get; set; }

        [Required]
        [Range(1, 20, ErrorMessage = "please select valid city")]
        public int city_id { get; set;
        }
    }
}
