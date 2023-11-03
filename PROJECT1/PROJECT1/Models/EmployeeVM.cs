using System.ComponentModel.DataAnnotations;

namespace PROJECT1.Models
{
    public class EmployeeVM
    {
        [Key]
        public int Id { get; set; }

      
        public string Name { get; set; }

     
        public string Email { get; set; }

        public string Mobile { get; set; }

        public string Gender { get; set; }

        public string country { get; set; }

        public string state { get; set; }

        public string city { get; set; }
    }
}
