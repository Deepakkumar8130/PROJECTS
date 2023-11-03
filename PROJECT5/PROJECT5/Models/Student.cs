

using System.ComponentModel.DataAnnotations;

namespace PROJECT5.Models
{
    public class Student
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
		public string Gender { get; set; }

        [Required(ErrorMessage = "please select a course")]
        public string Course { get; set; }
    }
}
    