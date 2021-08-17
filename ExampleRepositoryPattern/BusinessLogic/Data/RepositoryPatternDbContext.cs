using ExampleRepositoryPattern.Core;
using Microsoft.EntityFrameworkCore;

namespace ExampleRepositoryPattern.BusinessLogic.Data
{
    public class RepositoryPatternDbContext : DbContext
    {
        public RepositoryPatternDbContext(DbContextOptions options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
    }
}
