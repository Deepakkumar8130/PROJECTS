

using System.ComponentModel.DataAnnotations;

namespace PROJECT2.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage ="please enter a name")]
        [StringLength(100, MinimumLength = 3)]
        public string Name { get; set; }

        [Required(ErrorMessage = "please enter a mail")]
        [DataType(DataType.EmailAddress)]
        [MaxLength(50)]
        public string Email { get; set; }
        
        [Required(ErrorMessage = "please enter a phone no.")]
        [StringLength(10,MinimumLength = 10, ErrorMessage ="phone must have be 10 digit")]
		public string Phone { get; set; }

        [Required(ErrorMessage = "please select a gender")]
		public int? Gender { get; set; }

        [Required(ErrorMessage = "please enter a salary")]
        [Range(10000, 10000000, ErrorMessage = "Salary must be between 10000 and 10000000")]
        public double? Salary { get; set; }

        [Required(ErrorMessage = "please select a department")]
        public int? Department { get; set; }
    }
}
    