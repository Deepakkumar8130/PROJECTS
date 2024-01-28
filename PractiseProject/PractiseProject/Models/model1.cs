using System.ComponentModel.DataAnnotations;

namespace PractiseProject.Models
{
    public class model1
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Date { get; set; }
        public string? Age { get; set; }
        public string? Gender { get; set; }
        public string? Current_Address { get; set; }
        public string? Permanent_Address { get; set; }
    }
}
