using Microsoft.EntityFrameworkCore;
using PractiseProject.Models;

namespace PractiseProject.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<model1> Records { get; set; }

    }
}
