using Microsoft.EntityFrameworkCore;

namespace InternIntelligence_Portfolio.Models.DataBase
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Achievements> Achievement { get; set; }
        public DbSet<Contact_Form> ContactForms { get; set; }
        public DbSet<Projects> Project { get; set; }
        public DbSet<Skills> Skill { get; set; }
    }
}
