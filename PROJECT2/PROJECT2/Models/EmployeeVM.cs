using System.ComponentModel.DataAnnotations;

namespace PROJECT2.Models
{
    public class EmployeeVM
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Phone { get; set; }

        [Required]
        public string Gender { get; set; }

        [Required]
        public double Salary { get; set; }
        [Required]
        public string Department { get; set; }
    }
}
