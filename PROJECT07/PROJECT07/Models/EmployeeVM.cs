using System.ComponentModel.DataAnnotations;

namespace PROJECT07.Models
{
    public class EmployeeVM
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
        [MaxLength(500)]
        public string profileImg { get; set; }
        [Required]
        public string country { get; set; }
        [Required]
        public string state { get; set; }
        [Required]
        public string city { get; set; }
    }
}
