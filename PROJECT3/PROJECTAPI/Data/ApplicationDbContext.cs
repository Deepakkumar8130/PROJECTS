using Microsoft.EntityFrameworkCore;
using PROJECTAPI.Models;

namespace PROJECTAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Student> students { get; set; }
        public DbSet<UserModel> Users { get; set; }
        public DbSet<Course> courses { get; set; }

    }
}
