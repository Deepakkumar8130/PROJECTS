using Microsoft.EntityFrameworkCore;
using PROJECT5.Models;

namespace PROJECT5.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Student> Students { get; set; }

    }
}
