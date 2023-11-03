using System.ComponentModel.DataAnnotations;

namespace PROJECT07.Models
{
    public class Employee
    {
        [Key]
        public int id { get; set; }

        [Required]
        public string name { get; set; }

        [EmailAddress]
        public string email { get; set; }

        [Required]
        public string age { get; set; }

        [Required]
        public string gender { get; set; }

        
        public string profileImg { get; set; }

        [Required]
        public int country { get; set; }

        [Required]
        public int state { get; set; }

        [Required]
        public int city { get; set; }
    }
}
