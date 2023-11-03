using System.ComponentModel.DataAnnotations;

namespace PROJECT1.Models
{
    public class Country
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
