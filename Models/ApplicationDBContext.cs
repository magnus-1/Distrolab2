using Microsoft.EntityFrameworkCore;

namespace community.Models
{
    public class ApplicationDBContext : DbContext{
        public DbSet<Conference> Conferences { get; set; }
        public DbSet<Session> Sessions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder oprionsBuilder){
            oprionsBuilder.UseSqlite("Filename=.webNumberTwo.db");
        }
    }
}